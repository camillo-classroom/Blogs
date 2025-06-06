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

    public async Task<bool> SlugJaUtilizadoAsync(string slug, int idUsuarioAtual)
    {
        var sql = "SELECT COUNT(*) FROM usuario WHERE slug=@Slug AND id<>@idUsuarioAtual";

        var qtdeRegistros = await SelecionarUnicoAsync<int>(sql, new { Id = idUsuarioAtual, Slug = slug });

        return qtdeRegistros > 0;
    }
}
