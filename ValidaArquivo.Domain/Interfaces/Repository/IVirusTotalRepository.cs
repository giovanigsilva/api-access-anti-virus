using ValidaArquivo.Domain.Entities;
using ValidaArquivo.Domain.Responses.VirusTotal;

namespace ValidaArquivo.Domain.Interfaces.Repository
{
    public interface IVirusTotalRepository
    {
        public Task<ScanResponse> Scan(ArquivoUpload arquivo);
        public Task<ReportResponse> Report(ArquivoUpload arquivo);
    }
}
