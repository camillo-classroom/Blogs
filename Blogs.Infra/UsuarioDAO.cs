using System;
using Blogs.Model;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Infra;
//IPasswordHasher<Usuario> hasher
public class UsuarioDAO : BaseDAO<Usuario>, IUsuarioDAO
{
    protected override string NomeTabela => "usuario";

    public bool Login(Login login, IPasswordHasher<Usuario> hasher)
    {
        var sql = "SELECT * FROM usuario WHERE email=@Email";

        var usuario = SelecionarUnico(sql, login);

        if (usuario == null)
            return false;

        if (hasher.VerifyHashedPassword(usuario, usuario.HashSenha, login.Senha) == PasswordVerificationResult.Failed)
            return false;

        return true;
    }
}
