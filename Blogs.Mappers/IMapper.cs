using Blogs.DTO;
using Blogs.DTO.ControleAcessos;
using Blogs.Model;
using Blogs.Model.ControleAcessos;

namespace Blogs.Mappers;

public interface IMapper<TModel, TDto>
{
    TDto GetDto(TModel model);
    void PreencherModel(TModel model, TDto dto);
}
