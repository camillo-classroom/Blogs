using Blogs.Model.Postagens;

namespace Blogs.Infra.Postagens;
public class PostagemDAO : BaseDAO<Postagem>, IPostagemDAO
{
    protected override string NomeTabela => "postagem";

    public Task Ocultar(long id)
    {
        throw new NotImplementedException();
    }
}
