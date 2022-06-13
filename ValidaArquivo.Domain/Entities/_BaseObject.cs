using System.ComponentModel.DataAnnotations;

namespace ValidaArquivo.Domain.Entities
{

    /// <summary>
    /// Classe base para entidades que serão armazenadas em banco de dados
    /// </summary>
    public abstract class BaseObject
    {
        /// <summary>
        /// Id do registro
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Data de Criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Data da última atualização do registro
        /// </summary>
        public DateTime? DataAlteracao { get; set; }
    }
}
