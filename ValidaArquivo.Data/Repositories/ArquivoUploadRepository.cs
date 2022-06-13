using Microsoft.EntityFrameworkCore;
using ValidaArquivo.Domain.Entities;
using ValidaArquivo.Domain.Interfaces.Repository;

namespace ValidaArquivo.Data.Repositories
{
    /// <summary>
    /// Repositório específico para a classe/entidade ArquivoUpload
    /// </summary>
    public class ArquivoUploadRepository : BaseSQLRepository<ArquivoUpload>, IArquivoUploadRepository
    {
        public ArquivoUploadRepository(ValidaArquivoContext context) : base(context)
        {

        }

        /// <summary>
        /// Substituição do método genérico para retornar o arquivo incluindo os resultados da análise
        /// </summary>
        /// <param name="id">Id do objeto a ser retornado</param>
        /// <returns>Instância de ArquivoUpload de acordo com o Id informado</returns>
        public override ArquivoUpload? GetById(int id)
        {
            return context.Set<ArquivoUpload>()
                .Include(a => a.AnaliseArquivos)
                .SingleOrDefault(a => a.Id == id);
        }
    }
}
