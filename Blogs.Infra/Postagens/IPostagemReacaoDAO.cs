using Blogs.Model.Postagens;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Infra.Postagens;

public interface IPostagemReacaoDAO : IBaseDAO<PostagemReacao>
{
    Task AlterarReacaoAsync(PostagemReacao obj, PostagemReacao.EReacao reacao);

    Task ReagirAsync(PostagemReacao obj);

    Task<PostagemReacao?> RetornarPorIdUsuarioReacaoEIdPostagemAsync(long idUsuarioLogado, long id);
}
