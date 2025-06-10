using Blogs.Model.Postagens;

namespace Blogs.Infra.Postagens;
public class PostagemDAO : BaseDAO<Postagem>, IPostagemDAO
{
    protected override string NomeTabela => "postagem";

    public async Task Ocultar(long id)
    {
        string sql = $"UPDATE {NomeTabela}" +
            $" SET oculto = 1" +
            " WHERE " +
            " id = @Id";

        await ExecutarAsync(sql, new { Id = id });
    }

    public virtual async Task<IEnumerable<Postagem>> RetornarComPaginacaoDescendenteAsync(long idAutor, long? ultimoIdConsultado, int numeroRegsASeremRetornados = 100)
    {
        var campos = "";

        if (ultimoIdConsultado == null)
            ultimoIdConsultado = long.MaxValue;

        foreach (var nomeProp in GetPropriedades(typeof(Postagem)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT id as Id{campos}" +
            $" FROM {NomeTabela} WHERE idautor = @IdAutor AND oculto = 0 AND id < @Id ORDER BY id DESC" +
            $" LIMIT {numeroRegsASeremRetornados}";

        return await SelecionarAsync(sql, new { IdAutor = idAutor, Id = ultimoIdConsultado });
    }
}
