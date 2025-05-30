using Blogs.Model.ControleAcessos;

namespace Blogs.Infra.ControlesAcessos;

public interface IUsuarioDAO : IBaseDAO<Usuario>
{
    Task<Usuario?> RetornarPorEmailAsync(string email);
}
