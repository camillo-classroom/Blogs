using Blogs.DTO.Postagens;
using Blogs.Infra.Postagens;
using Blogs.Model.Postagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.UseCases.Postagens
{
    public interface IPostagemReacaoUseCase
    {
        Task<ResultadoVoid> Like(long postagemId);
        Task<ResultadoVoid> Deslike(long postagemId);
    }
}
