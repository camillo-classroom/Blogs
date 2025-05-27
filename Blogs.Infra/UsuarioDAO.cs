using System;
using Blogs.Model;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Infra;

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

    public void AlterarSenha(long idUsuario, string senhaAnterior, string senhaNova, IPasswordHasher<Usuario> hasher)
    {
        var sql = "SELECT * FROM usuario WHERE id=@Id";

        var usuario = SelecionarUnico(sql, new { Id = idUsuario });

        if (usuario == null)
            throw new Exception("Usuário não encontrado");

        if (hasher.VerifyHashedPassword(usuario, usuario.HashSenha, senhaAnterior) == PasswordVerificationResult.Failed)
            throw new Exception("Senha anterior incorreta");

        usuario.HashSenha = hasher.HashPassword(usuario, senhaNova);

        Executar("UPDATE usuario SET hash_senha=@HashSenha WHERE id=@Id", usuario);
    }
}
