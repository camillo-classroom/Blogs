using Blogs.Model.ControleAcessos;
using System.Text.Json.Serialization;

namespace Blogs.Model.Postagens;

public class Postagem : BaseModel
{
    public long IdAutor 
    {
        get => Autor?.Id ?? idAutor;
        set => idAutor = value;
    }

    [IgnoreEsteCampo]
    public Usuario? Autor { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public int Likes { get; set; }
    public int Deslikes { get; set; }
    public int Oculto { get; set; }

    private long idAutor;
}
