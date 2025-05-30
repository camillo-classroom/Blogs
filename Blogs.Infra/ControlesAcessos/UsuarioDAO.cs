using Blogs.Model.ControleAcessos;

namespace Blogs.Infra.ControlesAcessos;

public class UsuarioDAO : BaseDAO<Usuario>, IUsuarioDAO
{
    protected override string NomeTabela => "usuario";

    public async Task<Usuario?> RetornarPorEmailAsync(string email)
    {
        var sql = "SELECT * FROM usuario WHERE email=@Email";

        return await SelecionarUnicoAsync(sql, new { Email = email });
    }
}
