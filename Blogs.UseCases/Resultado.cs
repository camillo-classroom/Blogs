using Blogs.DTO.Postagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.UseCases;

public record ResultadoVoid (bool Sucesso, IEnumerable<MensagemRetorno>? Erros)
{
    
}

public record ResultadoUnico<T>(bool Sucesso, T? Objeto, IEnumerable<MensagemRetorno>? Erros)
{

}

public record ResultadoLista<T>(bool Sucesso, IEnumerable<T>? Objetos, IEnumerable<MensagemRetorno>? Erros)
{

}
