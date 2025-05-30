using Blogs.Model;

namespace Blogs.Infra;

public interface IBaseDAO<T> where T: IModel 
{
    Task InserirAsync(T obj);

    Task AlterarAsync(T obj);

    Task ExcluirAsync(long id);

    Task<IEnumerable<T>> RetornarComPaginacaoDescendenteAsync(long ultimoIdConsultado, int numeroRegsASeremRetornados = 100);

    Task<T?> RetornarPorIdAsync(long id);
}
