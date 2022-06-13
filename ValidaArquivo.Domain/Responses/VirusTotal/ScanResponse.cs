using Newtonsoft.Json;

namespace ValidaArquivo.Domain.Responses.VirusTotal
{
    /// <summary>
    /// Resposta da api /file/scan do Virus Total
    /// </summary>
    public class ScanResponse
    {
        /// <summary>
        /// Link para a análise no site
        /// </summary>
        public string Permalink { get; set; }
        
        /// <summary>
        /// Id do recurso
        /// </summary>
        public string Resource { get; set; }
        
        /// <summary>
        /// Código de resposta da análise
        /// </summary>
        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }
        
        /// <summary>
        /// Código da análise
        /// </summary>
        [JsonProperty("scan_id")]
        public string ScanId { get; set; }
        
        /// <summary>
        /// Mensagem de retorno
        /// </summary>
        [JsonProperty("verbose_msg")]
        public string VerboseMessage { get; set; }
        
        /// <summary>
        /// Hash do arquivo
        /// </summary>
        public string sha256 { get; set; }
    }
}
