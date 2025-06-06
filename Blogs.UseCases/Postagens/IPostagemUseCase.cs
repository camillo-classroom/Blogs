using Blogs.DTO.Postagens;

namespace Blogs.UseCases.Postagens
{
    public interface IPostagemUseCase
    {
        void IdentificarAcesso(long idUsuario);
        Task<ResultadoLista<PostagemDTO>> ConsultarPostagensAsync(long idAutor, long ultimoIdPostagemConsultado);
        Task<ResultadoVoid> AlterarPostagem(PostagemDTO postagem);
        Task<ResultadoVoid> ExcluirPostagem(long id);
        Task<ResultadoUnico<PostagemDTO>> InserirPostagem(PostagemDTO postagem);
    }
}
