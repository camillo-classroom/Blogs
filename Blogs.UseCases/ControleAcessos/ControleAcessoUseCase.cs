using Blogs.DTO.ControleAcessos;
using Blogs.Infra.ControlesAcessos;
using Blogs.Mappers;
using Blogs.Model.ControleAcessos;
using Microsoft.AspNetCore.Identity;

namespace Blogs.UseCases.ControleAcessos;

public class ControleAcessoUseCase
(
    IUsuarioDAO usuarioDAO, 
    IMapper<Usuario, UsuarioDTO> usuarioMapper, 
    IPasswordHasher<Usuario> hasher
) : BaseUseCase
{
    #region Todos_Usuarios

    public async Task<ResultadoVoid> LogarAsync(LoginDTO login)
    {
        try
        {
            var usuario = await usuarioDAO.RetornarPorEmailAsync(login.Email);

            if (usuario == null)
                return Falha([new("Usuário e/ou senha inválidos!")]);

            if (hasher.VerifyHashedPassword(usuario, usuario.HashSenha, login.Senha) == PasswordVerificationResult.Failed)
                return Falha([new("Usuário e/ou senha inválidos!")]);

            return Sucesso();
        }
        catch
        {
            return Falha([new("Erro no login.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoVoid> TrocarSenhaAsync(TrocarSenhaDTO trocarSenha)
    {
        try
        {
            var usuario = await usuarioDAO.RetornarPorEmailAsync(trocarSenha.Email);

            if (usuario == null)
                return Falha([new("Usuário e/ou senha inválidos!")]);

            if (hasher.VerifyHashedPassword(usuario, usuario.HashSenha, trocarSenha.SenhaAntiga) == PasswordVerificationResult.Failed)
            {
                return Falha([new("Usuário e/ou senha inválidos!")]);
            }

            usuario.HashSenha = hasher.HashPassword(usuario, trocarSenha.NovaSenha);

            return Sucesso();
        }
        catch
        {
            return Falha([new("Erro no login.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoVoid> InserirUsuario(UsuarioDTO usuario)
    {
        try
        {
            var obj = new Usuario();
            usuarioMapper.PreencherModel(obj, usuario);
            //usuario.Id = é preciso gerar novo id aqui...
            await usuarioDAO.InserirAsync(obj);

            return Sucesso();
        }
        catch
        {
            return Falha([new("Erro na tentativa de inserir novo usuário.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    #endregion

    #region Apenas_Logados

    public async Task<ResultadoVoid> AlterarUsuario(UsuarioDTO usuario)
    {
        if (idUsuarioLogado != usuario.Id)
            return Falha([new("Acesso não permitido.")]);
        
        try
        {
            var obj = await usuarioDAO.RetornarPorIdAsync(usuario.Id);

            if (obj == null)
                return Falha([new("O usuário que você deseja alterar não foi encontrado no sistema.")]);

            usuarioMapper.PreencherModel(obj, usuario);
            await usuarioDAO.AlterarAsync(obj);
            
            return Sucesso();
        }
        catch
        {
            return Falha([new("Erro na tentativa de alterar novo usuário.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    #endregion
}
