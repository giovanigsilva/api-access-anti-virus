using ValidaArquivo.Domain.Entities;
using ValidaArquivo.Domain.Interfaces.Repository;

namespace ValidaArquivo.Data.Repositories
{
    /// <summary>
    /// Repositório genérico para manipulação dos registros no banco de dados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSQLRepository<T> : IBaseSQLRepository<T>
        where T : BaseObject
    {
        protected readonly ValidaArquivoContext context;

        protected BaseSQLRepository(ValidaArquivoContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtém um registro pelo seu id
        /// </summary>
        /// <param name="id">id do registro procurado</param>
        /// <returns>instância do registro confirme gravado no banco de dados</returns>
        public virtual T? GetById(int id)
        {
            return context.Set<T>().SingleOrDefault(obj => obj.Id == id);
        }

        /// <summary>
        /// Disponibiliza uma interface de consulta para outros serviços
        /// </summary>
        /// <returns>Interface de consulta de uma determinada tabela no banco de dados</returns>
        public IQueryable<T> Query()
        {
            return context.Set<T>().AsQueryable();
        }

        /// <summary>
        /// Insere um registro no banco de dados
        /// </summary>
        /// <param name="entity">Objeto a ser inserido</param>
        public void Incluir(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Altera o registro de um objeto no banco de dados
        /// </summary>
        /// <param name="arquivo">Objeto a ser alterado</param>
        public void Editar(T arquivo)
        {
            context.Entry(arquivo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
