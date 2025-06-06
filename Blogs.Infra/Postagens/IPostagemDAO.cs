using Blogs.Model.Postagens;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Infra.Postagens;

public interface IPostagemDAO : IBaseDAO<Postagem>
{
    Task<IEnumerable<Postagem>> RetornarComPaginacaoDescendenteAsync(long idAutor, long ultimoIdConsultado, int numeroRegsASeremRetornados = 100);
    Task Ocultar(long id);
}
