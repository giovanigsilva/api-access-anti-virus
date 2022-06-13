using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ValidaArquivo.Domain.Responses.VirusTotal
{
    /// <summary>
    /// Retorno da api /file/report do Virus Total
    /// </summary>
    public class ReportResponse
    {
        /// <summary>
        /// Código de resposta
        /// </summary>
        public int response_code { get; set; }

        /// <summary>
        /// Mensagem de retorno da análise
        /// </summary>
        public string verbose_msg { get; set; }

        /// <summary>
        /// Código do recurso
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Código da análise
        /// </summary>
        public string scan_id { get; set; }

        /// <summary>
        /// Hash MD5 do arquivo
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// Hash SHA1 do arquivo
        /// </summary>
        public string sha1 { get; set; }

        /// <summary>
        /// Hash SHA256 do arquivo
        /// </summary>
        public string sha256 { get; set; }

        /// <summary>
        /// Data do escaneamento
        /// </summary>
        public string scan_date { get; set; }

        /// <summary>
        /// Link da análise
        /// </summary>
        public string permalink { get; set; }

        /// <summary>
        /// Quantidade de ameaças encontradas
        /// </summary>
        public int positives { get; set; }

        /// <summary>
        /// Total de algorítmos utilizados para a análise
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// Resultado dos algorítmos utilizados
        /// </summary>
        public JObject scans { get; set; }
    }

    /// <summary>
    /// Resultado de análise por um algorítmo
    /// </summary>
    public class Scan
    {
        /// <summary>
        /// Ameaça detectada
        /// </summary>
        public bool detected { get; set; }

        /// <summary>
        /// Versão do algorítmo
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// Descrição da ameaça
        /// </summary>
        public string? result { get; set; }

        /// <summary>
        /// Data da versão do algorítmo
        /// </summary>
        public string update { get; set; }
    }
}
