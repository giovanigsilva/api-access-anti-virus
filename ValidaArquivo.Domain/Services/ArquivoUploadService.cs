using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using ValidaArquivo.Domain.Commands.ArquivoUpload;
using ValidaArquivo.Domain.Entities;
using ValidaArquivo.Domain.Interfaces.Repository;
using ValidaArquivo.Domain.Responses.ArquivoUpload;
using ValidaArquivo.Domain.Responses.VirusTotal;

namespace ValidaArquivo.Domain.Services
{
    /// <summary>
    /// Regras de negócio para a entidade ArquivoUpload
    /// </summary>
    public class ArquivoUploadService : BaseService
    {
        private readonly IArquivoUploadRepository _arquivoUploadRepository;
        private readonly IVirusTotalRepository _virusTotalRepository;

        public ArquivoUploadService(
            IArquivoUploadRepository arquivoUploadRepository,
            IVirusTotalRepository virusTotalRepository
            )
        {
            _arquivoUploadRepository = arquivoUploadRepository;
            _virusTotalRepository = virusTotalRepository;
        }

        /// <summary>
        /// Obtém a data de retorno de análise mais recente
        /// </summary>
        /// <returns></returns>
        public DateTime? ObterDataUltimaAtualizacaoVirusTotal()
        {
            var query = _arquivoUploadRepository.Query();
            if (query.All(a => !a.DataResultadoVirusTotal.HasValue))
                return null;

            return query.Max(a => a.DataResultadoVirusTotal);
        }

        /// <summary>
        /// Obtém o ArquivoUpload do banco de dados de acordo com o Id informado
        /// </summary>
        /// <param name="id">Id do arquivo a ser retornado</param>
        /// <returns></returns>
        public ArquivoUpload Detalhar(int id)
        {
            return _arquivoUploadRepository.GetById(id);
        }

        /// <summary>
        /// Retorna uma lista dos arquivos para exibição em tela.
        /// </summary>
        /// <returns>Lista otimizada com apenas os dados necessários para a tela de listagem</returns>
        public IEnumerable<ResumoArquivoResponse> ListarResumoArquivos()
        {
            var query = _arquivoUploadRepository.Query();
            return query.OrderByDescending(a => a.DataCriacao).Select(a => new ResumoArquivoResponse()
            {
                Id = a.Id,
                NomeArquivo = a.NomeArquivo,
                DataUploadArquivo = a.DataCriacao,
                DataEnvioAnalise = a.DataEnvioVirusTotal,
                DataResultado = a.DataResultadoVirusTotal,
                Deteccoes = a.AnaliseArquivos.Count(aa=>aa.Detected),
                Status = a.Status
            }).ToList();
        }

        /// <summary>
        /// Recebe os arquivos inputados, registra no banco de dados e envia para a fila de análise do Virus Total
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task ProcessarArquivo(ProcessarArquivoCommand command)
        {
            foreach (var file in command.Files)
            {
                var arquivoUpload = new ArquivoUpload();
                arquivoUpload.NomeArquivo = file.FileName;
                arquivoUpload.Conteudo = file.Content;

                await EnviarParaFila(arquivoUpload);
                _arquivoUploadRepository.Incluir(arquivoUpload);
            }
        }

        /// <summary>
        /// Busca por arquivos pendentes de envio e envia novamente para a fila do Virus Total
        /// </summary>
        /// <returns></returns>
        public async Task ReenviarPendentes()
        {
            var pendentes = (from arquivo in _arquivoUploadRepository.Query()
                             where !arquivo.DataEnvioVirusTotal.HasValue
                             select arquivo).ToList();

            foreach (var arquivo in pendentes)
            {
                await EnviarParaFila(arquivo);
                _arquivoUploadRepository.Editar(arquivo);
            }
        }

        /// <summary>
        /// Busca por arquivos que não possuem resultados de análise e inicia uma integração com o Virus Total para retorná-lo
        /// </summary>
        /// <returns></returns>
        public async Task BuscarResultados()
        {
            var aguardandoVirusTotal = (from arquivo in _arquivoUploadRepository.Query()
                                        where !arquivo.DataResultadoVirusTotal.HasValue
                                        select arquivo).ToList();

            foreach (var arquivo in aguardandoVirusTotal)
            {
                await ObterRetornoAnalise(arquivo);
                _arquivoUploadRepository.Editar(arquivo);
            }
        }

        /// <summary>
        /// Envia para a fila de análise do vírus total e armazena o progresso no banco de dados
        /// </summary>
        /// <param name="arquivoUpload"></param>
        /// <returns></returns>
        private async Task EnviarParaFila(ArquivoUpload arquivoUpload)
        {
            try
            {
                var scanResult = await _virusTotalRepository.Scan(arquivoUpload);
                arquivoUpload.DataEnvioVirusTotal = DateTime.Now;

                arquivoUpload.VirusTotalPermalink = scanResult.Permalink;
                arquivoUpload.VirusTotalResource = scanResult.Resource;
                arquivoUpload.VirusTotalResponseCode = scanResult.ResponseCode;
                arquivoUpload.VirusTotalScanId = scanResult.ScanId;
                arquivoUpload.VirusTotalVerboseMessage = scanResult.VerboseMessage;
                arquivoUpload.VirusTotalSHA256 = scanResult.sha256;

                arquivoUpload.ErroEnvioVirusTotal = null;
            }
            catch (Exception ex)
            {
                arquivoUpload.DataEnvioVirusTotal = null;
                arquivoUpload.ErroEnvioVirusTotal = ex.Message;
            }
        }

        /// <summary>
        /// Obtém o retorno da análise do Virus Total e os armazena no banco de dados
        /// </summary>
        /// <param name="arquivoUpload"></param>
        /// <returns></returns>
        private async Task ObterRetornoAnalise(ArquivoUpload arquivoUpload)
        {
            try
            {
                var reportResult = await _virusTotalRepository.Report(arquivoUpload);

                foreach (JProperty scan in reportResult.scans.Children())
                {
                    var antivirus = scan.Name;
                    var scanData = scan.Value.ToObject<Scan>();
                    if (scanData == null)
                        continue;

                    var analise = new AnaliseArquivo();

                    analise.Antivirus = antivirus;
                    analise.Detected = scanData.detected;
                    analise.Version = scanData.version;
                    
                    if (scanData.result != null)
                        analise.Result = scanData.result;

                    analise.Update = scanData.update;

                    if(arquivoUpload.AnaliseArquivos == null)
                        arquivoUpload.AnaliseArquivos = new Collection<AnaliseArquivo>();

                    arquivoUpload.AnaliseArquivos.Add(analise);
                }

                arquivoUpload.DataResultadoVirusTotal = DateTime.Now;
                arquivoUpload.ErroRetornoVirusTotal = null;
            }
            catch (Exception ex)
            {
                arquivoUpload.DataResultadoVirusTotal = null;
                arquivoUpload.ErroRetornoVirusTotal = ex.Message;
            }
        }
    }
}
