using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ValidaArquivo.Domain.Commands.ArquivoUpload;
using ValidaArquivo.Domain.Services;
using ValidaArquivo.Web.Models;

namespace ValidaArquivo.Web.Controllers
{
    /// <summary>
    /// Controller responsável por apresentar as páginas da aplicação
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArquivoUploadService _arquivoUploadService;

        public HomeController(
            ILogger<HomeController> logger,
            ArquivoUploadService arquivoUploadService
            )
        {
            _logger = logger;
            _arquivoUploadService = arquivoUploadService;
        }

        /// <summary>
        /// Carrega os dados e aciona a listagem de arquivos
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var arquivos = _arquivoUploadService.ListarResumoArquivos();
            ViewBag.DataUltimaAtualizacao = _arquivoUploadService.ObterDataUltimaAtualizacaoVirusTotal();

            return View(arquivos);
        }

        /// <summary>
        /// Carrega um arquivo específico e aciona os detalhes do arquivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detalhes(int id)
        {
            var arquivo = _arquivoUploadService.Detalhar(id);
            return View(arquivo);
        }

        /// <summary>
        /// Aciona a tela de envio de arquivos
        /// </summary>
        /// <returns></returns>
        public IActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// Processa os arquivos enviados e volta para a tela de listagem
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProcessarUpload(IFormCollection collection)
        {
            await _arquivoUploadService.ProcessarArquivo(ProcessarArquivoCommand.FromCollection(collection));
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Tratamento e apresentação genérica de erros
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}