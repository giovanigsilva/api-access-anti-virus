namespace ValidaArquivo.Domain.Entities
{
    /// <summary>
    /// Classe para armazenar os dados dos arquivos
    /// </summary>
    public class ArquivoUpload : BaseObject
    {
        /// <summary>
        /// Nome do arquivo
        /// </summary>
        public string NomeArquivo { get; set; }

        /// <summary>
        /// Conteúdo do arquivo em array de bytes
        /// </summary>
        public byte[] Conteudo { get; set; }

        #region Resultado do envio dos dados para o Virus Total
        /// <summary>
        /// Data de envio para o vírus total
        /// </summary>
        public DateTime? DataEnvioVirusTotal { get; set; }
        
        /// <summary>
        /// Link da análise
        /// </summary>
        public string? VirusTotalPermalink { get; set; }

        /// <summary>
        /// Código do recurso
        /// </summary>
        public string? VirusTotalResource { get; set; }

        /// <summary>
        /// Código da resposta da análise
        /// </summary>
        public int? VirusTotalResponseCode { get; set; }

        /// <summary>
        /// Código da análise
        /// </summary>
        public string? VirusTotalScanId { get; set; }

        /// <summary>
        /// Mensagem de retorno da análise
        /// </summary>
        public string? VirusTotalVerboseMessage { get; set; }

        /// <summary>
        /// Hash SHA256 do arquivo
        /// </summary>
        public string? VirusTotalSHA256 { get; set; }

        /// <summary>
        /// Descrição do erro ao enviar o arquivo para análise
        /// </summary>
        public string? ErroEnvioVirusTotal { get; set; }
        #endregion

        #region Resultado da análise do Virus Total
        /// <summary>
        /// Data da análise do arquivo
        /// </summary>
        public DateTime? DataResultadoVirusTotal { get; set; }

        /// <summary>
        /// Descrição do erro ao tentar obter o resultado da análise
        /// </summary>
        public string? ErroRetornoVirusTotal { get; set; }

        /// <summary>
        /// Análises realizadas no arquivo
        /// </summary>
        public ICollection<AnaliseArquivo> AnaliseArquivos { get; set; }
        #endregion
        
        /// <summary>
        /// Status de processamento do arquivo
        /// </summary>
        public string Status
        {
            get
            {
                if (DataResultadoVirusTotal.HasValue)
                    return "Processado";
                else if (DataEnvioVirusTotal.HasValue)
                    return "Aguardando retorno";
                return "Envio pendente";
            }
        }
    }
}
