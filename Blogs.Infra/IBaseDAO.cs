using Blogs.Model;

namespace Blogs.Infra;

public interface IBaseDAO<T> where T: IModel 
{
    Task InserirAsync(T obj);

    Task AlterarAsync(T obj);

    Task ExcluirAsync(long id);

    Task<T?> RetornarPorIdAsync(long id);
}
