using Blogs.Model;

namespace Blogs.Infra;

public interface IBaseDAO<T> where T: IModel 
{
    public void Inserir(T obj);
    public void Alterar(T obj);
    public void Excluir(long id);
    public IList<T> RetornarTodos();
    public T? RetornarPorId(long id);
}
