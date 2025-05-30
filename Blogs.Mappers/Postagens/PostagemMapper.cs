using Blogs.DTO.Postagens;
using Blogs.Model.Postagens;

namespace Blogs.Mappers.Postagens;

public class PostagemMapper : IMapper<Postagem, PostagemDTO>
{
    public PostagemDTO GetDto(Postagem model)
    {
        return new PostagemDTO
        (
            Id: model.Id,
            Titulo: model.Titulo,
            Conteudo: model.Conteudo,
            DataHora: model.DataHora,
            Likes: model.Likes,
            Deslikes: model.Deslikes
        );
    }

    public void PreencherModel(Postagem model, PostagemDTO dto)
    {
        model.Id = dto.Id;
        model.Titulo = dto.Titulo;
        model.Conteudo = dto.Conteudo;
        model.DataHora = dto.DataHora;
        model.Likes = dto.Likes;
        model.Deslikes = dto.Deslikes;
    }
}
