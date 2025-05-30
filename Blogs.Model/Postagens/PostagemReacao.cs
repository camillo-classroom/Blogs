namespace Blogs.Model.Postagens;

public class PostagemReacao : BaseModel
{
    public long IdPostagem { get; set; }
    public long IdUsuario { get; set; }
    public EReacao Reacao { get; set; }
    public DateTime DataHora { get; set; }

    public enum EReacao
    {
        Like = 1,
        Deslike = -1
    }
}
