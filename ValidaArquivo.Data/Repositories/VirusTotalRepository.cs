using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using ValidaArquivo.Domain.Entities;
using ValidaArquivo.Domain.Interfaces.Repository;
using ValidaArquivo.Domain.Responses.VirusTotal;

namespace ValidaArquivo.Data.Repositories
{
    public class VirusTotalRepository : IVirusTotalRepository
    {
        private readonly string _endpoint = "https://www.virustotal.com/vtapi/v2";
        private readonly string _apiKey;

        public VirusTotalRepository(
            IConfiguration configuration)
        {
            _apiKey = configuration["VirusTotalAPIKey"];
        }

        public async Task<ReportResponse> Report(ArquivoUpload arquivo)
        {
            var client = new RestClient(_endpoint);
            var request = new RestRequest("/file/report");
            request.AddParameter("apikey", _apiKey);
            request.AddParameter("resource", arquivo.VirusTotalResource);

            var response = await client.ExecuteGetAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<ReportResponse>(response.Content);

            throw new Exception(response.ErrorMessage ?? "Erro interno no serviço do Virus Total");
        }

        public async Task<ScanResponse> Scan(ArquivoUpload arquivo)
        {
            var client = new RestClient(_endpoint);

            var request = new RestRequest("/file/scan");
            request.AddHeader("Accept", "text/plain");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("apikey", _apiKey);
            request.AddParameter("file", Encoding.Default.GetString(arquivo.Conteudo));

            var response = await client.ExecutePostAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<ScanResponse>(response.Content);

            throw new Exception(response.ErrorMessage ?? "Erro interno no serviço do Virus Total");
        }
    }
}
