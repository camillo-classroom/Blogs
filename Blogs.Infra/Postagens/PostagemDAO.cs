using Blogs.Model.Postagens;

namespace Blogs.Infra.Postagens;
public class PostagemDAO : BaseDAO<Postagem>, IPostagemDAO
{
    protected override string NomeTabela => "postagem";

    public Task Ocultar(long id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<Postagem>> RetornarComPaginacaoDescendenteAsync(long idAutor, long ultimoIdConsultado, int numeroRegsASeremRetornados = 100)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(typeof(Postagem)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT TOP {numeroRegsASeremRetornados} id as Id{campos}" +
            $" FROM {NomeTabela} WHERE idautor = @IdAutor AND id < @Id ORDER BY id DESC";

        return await SelecionarAsync(sql, new { IdAutor = idAutor, Id = ultimoIdConsultado });
    }
}
