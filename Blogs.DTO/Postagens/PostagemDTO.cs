namespace Blogs.DTO.Postagens;

public record PostagemDTO
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
