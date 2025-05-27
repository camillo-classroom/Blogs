using Blogs.Model;

namespace Blogs.Infra;
public class PostagemDAO : BaseDAO<Postagem>, IPostagemDAO
{
    protected override string NomeTabela => "postagem";
}
