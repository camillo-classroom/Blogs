﻿using Blogs.DTO.ControleAcessos;
using Blogs.Model.ControleAcessos;

namespace Blogs.Mappers.ControleAcessos;

public class UsuarioMapper : IMapper<Usuario, UsuarioDTO>
{
    public UsuarioDTO GetDto(Usuario model)
    {
        return new UsuarioDTO
        (
            model.Id,
            model.Email,
            model.Nome,
            model.Slug
        );
    }

    public void PreencherModel(Usuario model, UsuarioDTO dto)
    {
        model.Id = dto.Id;
        model.Email = dto.Email;
        model.Nome = dto.Nome;
        model.Slug = dto.Slug;
    }
}
