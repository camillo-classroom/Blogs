using Blogs.Model;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Infra;

public interface IUsuarioDAO : IBaseDAO<Usuario>
{
    bool Login(Login login, IPasswordHasher<Usuario> hasher);
}
