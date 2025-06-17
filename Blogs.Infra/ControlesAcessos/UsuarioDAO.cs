using Blogs.Model.ControleAcessos;

namespace Blogs.Infra.ControlesAcessos;

public class UsuarioDAO : BaseDAO<Usuario>, IUsuarioDAO
{
    protected override string NomeTabela => "usuario";

    public async Task<Usuario?> RetornarPorSlugAsync(string slug)
    {
        var sql = "SELECT id FROM usuario WHERE slug=@Slug";

        return await SelecionarUnicoAsync(sql, new { Slug = slug });
    }

    public async Task<Usuario?> RetornarPorEmailAsync(string email)
    {
        var sql = "SELECT * FROM usuario WHERE email=@Email";

        return await SelecionarUnicoAsync(sql, new { Email = email });
    }

    public async Task<IEnumerable<Usuario>> RetornarPrincipaisAutoresAsync()
    {
        //Aqui deveria ser feita alguma checagem para verificar a popularidade do autor
        //Para fins de exemplo, estou retornando os 20 primeiros autores ordenados por nome
        var sql = "SELECT * FROM usuario ORDER BY nome LIMIT 20";

        return await SelecionarAsync(sql);
    }

    public async Task<bool> SlugJaUtilizadoAsync(string slug, int idUsuarioAtual)
    {
        var sql = "SELECT COUNT(*) FROM usuario WHERE slug=@Slug AND id<>@idUsuarioAtual";

        var qtdeRegistros = await SelecionarUnicoAsync<int>(sql, new { Id = idUsuarioAtual, Slug = slug });

        return qtdeRegistros > 0;
    }
}
