namespace Blogs.DTO.ControleAcessos;

public record struct UsuarioDTO(
    long Id,
    string Nome,
    string Email,
    string Slug,
    string Apresentacao = "Olá, este é o meu blog! Eu sou um desenvolvedor apaixonado por tecnologia e adoro compartilhar conhecimento. Espero que você goste do conteúdo que estou criando aqui! 😊"
)
{
}
