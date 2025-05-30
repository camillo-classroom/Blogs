using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.DTO.ControleAcessos;

public record TrocarSenhaDTO(
    string Email, 
    string SenhaAntiga, 
    string NovaSenha
)
{

}
