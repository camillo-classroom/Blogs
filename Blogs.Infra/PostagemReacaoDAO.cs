using Blogs.Model;

namespace Blogs.Infra;
public class PostagemReacaoDAO : BaseDAO<PostagemReacao>, IPostagemReacaoDAO
{
    protected override string NomeTabela => "postagem_reacao";
}
