using Blogs.Model.ControleAcessos;

namespace Blogs.Infra.ControlesAcessos;

public interface IUsuarioDAO : IBaseDAO<Usuario>
{
    Task<Usuario?> RetornarPorSlugAsync(string slugAutor);
    Task<Usuario?> RetornarPorEmailAsync(string email);
    Task<IEnumerable<Usuario>> RetornarPrincipaisAutoresAsync();
    Task<bool> SlugJaUtilizadoAsync(string slug, int idUsuarioAtual);
}
