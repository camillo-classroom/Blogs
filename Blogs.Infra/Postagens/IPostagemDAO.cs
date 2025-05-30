using Blogs.Model.Postagens;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Infra.Postagens;

public interface IPostagemDAO : IBaseDAO<Postagem>
{
    Task Ocultar(long id);
}
