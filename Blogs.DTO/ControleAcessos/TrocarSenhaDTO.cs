namespace Blogs.DTO.ControleAcessos;

public record struct TrocarSenhaDTO(
    string Email, 
    string SenhaAntiga, 
    string NovaSenha
)
{

}
