namespace Blogs.Model.ControleAcessos;

public class Usuario : BaseModel
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashSenha { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Apresentacao { get; set; } = string.Empty;
}
