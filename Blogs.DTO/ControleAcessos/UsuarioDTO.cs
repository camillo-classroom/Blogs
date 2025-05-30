using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.DTO.ControleAcessos;

public record UsuarioDTO(
    long Id,
    string Nome,
    string Email,
    string Slug
)
{
}
