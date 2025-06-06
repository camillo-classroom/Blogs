using Blogs.Infra.Postagens;
using Blogs.Model.Postagens;

namespace Blogs.UseCases.Postagens;

public class PostagemReacaoUseCase
(
    IPostagemDAO postagemDAO,
    IPostagemReacaoDAO postagemReacaoDAO
) : BaseUseCase, IPostagemReacaoUseCase
{
    public async Task<ResultadoVoid> Like(long postagemId)
    {
        if (idUsuarioLogado == 0)
            return Falha([new("Acesso não permitido.")]);

        var obj = await postagemDAO.RetornarPorIdAsync(postagemId);

        if (obj == null)
            return Falha([new("A postagem que você deseja reagir não foi encontrada no sistema.")]);
        
        var objReacao = await postagemReacaoDAO.RetornarPorIdUsuarioReacaoEIdPostagemAsync(idUsuarioLogado, obj.Id);

        if (objReacao == null)
        {
            objReacao = new PostagemReacao
            {
                IdPostagem = postagemId,
                IdUsuario = idUsuarioLogado,
                DataHora = DateTime.Now,
                Reacao = PostagemReacao.EReacao.Like
            };

            await postagemReacaoDAO.ReagirAsync(objReacao);
        }
        else
        {
            if (objReacao.Reacao == PostagemReacao.EReacao.Like)
                return Falha([new("Você já reagiu anteiormente à esta postagem.")]);

            objReacao.DataHora = DateTime.Now;            
            await postagemReacaoDAO.AlterarReacaoAsync(objReacao, PostagemReacao.EReacao.Like);
        }

        return Sucesso();
    }

    public async Task<ResultadoVoid> Deslike(long postagemId)
    {
        if (idUsuarioLogado == 0)
            return Falha([new("Acesso não permitido.")]);

        var obj = await postagemDAO.RetornarPorIdAsync(postagemId);

        if (obj == null)
            return Falha([new("A postagem que você deseja reagir não foi encontrada no sistema.")]);

        var objReacao = await postagemReacaoDAO.RetornarPorIdUsuarioReacaoEIdPostagemAsync(idUsuarioLogado, obj.Id);

        if (objReacao == null)
        {
            objReacao = new PostagemReacao
            {
                IdPostagem = postagemId,
                IdUsuario = idUsuarioLogado,
                DataHora = DateTime.Now,
                Reacao = PostagemReacao.EReacao.Deslike
            };

            await postagemReacaoDAO.ReagirAsync(objReacao);
        }
        else
        {
            if (objReacao.Reacao == PostagemReacao.EReacao.Deslike)
                return Falha([new("Você já reagiu anteiormente à esta postagem.")]);

            objReacao.DataHora = DateTime.Now;
            await postagemReacaoDAO.AlterarReacaoAsync(objReacao, PostagemReacao.EReacao.Deslike);
        }

        return Sucesso();
    }
}
