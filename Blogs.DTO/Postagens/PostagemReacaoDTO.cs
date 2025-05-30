using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.DTO.Postagens;

public record PostagemReacaoDTO
(
    long Id,
    long IdPostagem,
    long IdUsuario
)
{
    
}
