namespace ValidaArquivo.Domain.Responses.ArquivoUpload
{
    /// <summary>
    /// View model para exibição na tela de listagem de arquivos
    /// </summary>
    public class ResumoArquivoResponse
    {
        /// <summary>
        /// Id do arquivo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do arquivo
        /// </summary>
        public string NomeArquivo { get; set; }

        /// <summary>
        /// Data de upload do arquivo no sistema
        /// </summary>
        public DateTime DataUploadArquivo { get; set; }

        /// <summary>
        /// Data de upload do arquivo no Virus Total
        /// </summary>
        public DateTime? DataEnvioAnalise { get; set; }

        /// <summary>
        /// Data em que o resultado foi retornado
        /// </summary>
        public DateTime? DataResultado { get; set; }

        /// <summary>
        /// Status do arquivo
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Quantidade de ameaças encontradas
        /// </summary>
        public int Deteccoes { get; set; }
    }
}
