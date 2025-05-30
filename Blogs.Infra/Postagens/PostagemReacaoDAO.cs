using Blogs.Model.Postagens;
using Dapper;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using static Blogs.Model.Postagens.PostagemReacao;

namespace Blogs.Infra.Postagens;
public class PostagemReacaoDAO : BaseDAO<PostagemReacao>, IPostagemReacaoDAO
{
    protected override string NomeTabela => "postagem_reacao";

    public async Task AlterarReacaoAsync(PostagemReacao obj, PostagemReacao.EReacao reacao)
    {
        if (obj.Reacao == reacao)
            return;

        var antesEraLike = obj.Reacao == PostagemReacao.EReacao.Like;
        obj.Reacao = reacao;

        using (var conexao = new SqliteConnection("Data Source=db/app.db"))
        {
            try
            {
                await conexao.OpenAsync();

                using (var tran = conexao.BeginTransaction())
                {
                    try
                    {
                        if (antesEraLike)
                            await conexao.ExecuteAsync("UPDATE postagem SET likes = likes - 1, deslikes = deslikes + 1 WHERE id_postagem = @IdPostagem", obj);
                        else
                            await conexao.ExecuteAsync("UPDATE postagem SET deslikes = deslikes - 1, likes = likes + 1 WHERE id_postagem = @IdPostagem", obj);

                        //obj.Id = ... precisa adicionar o id
                        var sql = $"INSERT INTO {NomeTabela} (id_postagem, id_usuario, reacao, data_hora) VALUES (@IdPostagem, @IdUsuario, @Reacao, @DataHora)";

                        await conexao.ExecuteAsync(sql, obj);

                        await tran.CommitAsync();
                    }
                    catch
                    {
                        await tran.RollbackAsync();
                    }
                }
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }
    }

    public async Task ReagirAsync(PostagemReacao obj)
    {
        using (var conexao = new SqliteConnection("Data Source=db/app.db"))
        {
            try
            {
                await conexao.OpenAsync();

                using (var tran = conexao.BeginTransaction())
                {
                    try
                    {
                        if (obj.Reacao == EReacao.Like)
                            await conexao.ExecuteAsync("UPDATE postagem SET likes = likes + 1 WHERE id_postagem = @IdPostagem", obj);
                        else
                            await conexao.ExecuteAsync("UPDATE postagem SET deslikes = deslikes + 1 WHERE id_postagem = @IdPostagem", obj);

                        //obj.Id = ... precisa adicionar o id
                        var sql = $"INSERT INTO {NomeTabela} (id_postagem, id_usuario, reacao, data_hora) VALUES (@IdPostagem, @IdUsuario, @Reacao, @DataHora)";

                        await conexao.ExecuteAsync(sql, obj);

                        await tran.CommitAsync();
                    }
                    catch
                    {
                        await tran.RollbackAsync();
                    }
                }
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }
    }

    public async Task<PostagemReacao?> RetornarPorIdUsuarioReacaoEIdPostagemAsync(long idUsuarioLogado, long idPostagem)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(typeof(PostagemReacao)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT id as Id{campos}" +
            $" FROM {NomeTabela} WHERE id_usuario = IdUsuarioLogado AND id_postagem = @IdPostagem";

        return await SelecionarUnicoAsync(sql, new { IdUsuarioLogado = idUsuarioLogado, IdPostagem = idPostagem });
    }
}
