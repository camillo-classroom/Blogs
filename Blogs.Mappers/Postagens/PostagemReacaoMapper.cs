using Blogs.DTO.Postagens;
using Blogs.Model.Postagens;

namespace Blogs.Mappers.Postagens
{
    public class PostagemReacaoMapper : IMapper<PostagemReacao, PostagemReacaoDTO>
    {
        public PostagemReacaoDTO GetDto(PostagemReacao model)
        {
            //As reações são enviadas do front para o backend, mas nunca ao contrário.
            throw new NotImplementedException("Este recurso não deve ser encaminhado para o frontend.");
        }

        public void PreencherModel(PostagemReacao model, PostagemReacaoDTO dto)
        {
            model.Id = dto.Id;
            model.IdPostagem = dto.IdPostagem;
            model.IdUsuario = dto.IdUsuario;
        }
    }
}
