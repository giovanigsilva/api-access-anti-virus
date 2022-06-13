namespace ValidaArquivo.Domain.Entities
{
    /// <summary>
    /// Classe para armazenar o resultado da análise de um algorítmo
    /// </summary>
    public class AnaliseArquivo : BaseObject
    {
        /// <summary>
        /// Nome do algorítmo
        /// </summary>
        public string Antivirus { get;  set; }

        /// <summary>
        /// Ameaça detectada
        /// </summary>
        public bool Detected { get;  set; }

        /// <summary>
        /// Versão do antivírus
        /// </summary>
        public string Version { get;  set; }

        /// <summary>
        /// Descrição da ameaça
        /// </summary>
        public string? Result { get;  set; }

        /// <summary>
        /// Data da versão do antivírus
        /// </summary>
        public string Update { get;  set; }

        /// <summary>
        /// Referência do arquivo analisado
        /// </summary>
        public virtual ArquivoUpload ArquivoUpload { get; set; }

        /// <summary>
        /// Id do arquivo analisado
        /// </summary>
        public int ArquivoUploadId { get; set; }
    }
}
