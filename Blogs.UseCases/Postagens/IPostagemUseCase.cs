using Blogs.DTO.Postagens;

namespace Blogs.UseCases.Postagens
{
    public interface IPostagemUseCase
    {
        Task<ResultadoLista<PostagemDTO>> ConsultarPostagensAsync(long ultimoIdConsultado);
        Task<ResultadoVoid> AlterarPostagem(PostagemDTO postagem);
        Task<ResultadoVoid> ExcluirPostagem(PostagemDTO postagem);
        Task<ResultadoVoid> InserirPostagem(PostagemDTO postagem);
    }
}
