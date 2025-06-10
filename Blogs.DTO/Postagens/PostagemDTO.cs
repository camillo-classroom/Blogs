namespace Blogs.DTO.Postagens;

public record struct PostagemDTO
(
    long Id,
    string Titulo,
    string Conteudo,
    DateTime DataHora,
    int Likes,
    int Deslikes
)
{
}
