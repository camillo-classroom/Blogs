using System;

namespace Blogs.Model;

public class Postagem : BaseModel
{
    public Usuario? Autor { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public int Likes { get; set; }
    public int Delikes { get; set; }
}
