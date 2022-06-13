using ValidaArquivo.Domain.Entities;

namespace ValidaArquivo.Domain.Interfaces.Repository
{
    public interface IBaseSQLRepository<T> where T : BaseObject
    {
        T? GetById(int id);
        IQueryable<T> Query();
        void Incluir(T entity);
        void Editar(T arquivo);
    }
}
