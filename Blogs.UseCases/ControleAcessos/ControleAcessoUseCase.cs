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
) : BaseUseCase, IControleAcessoUseCase
{
    #region Todos_Usuarios

    public async Task<ResultadoLista<UsuarioDTO>> ObterPrincipaisAutores()
    {
        try
        {
            var objetos = await usuarioDAO.RetornarPrincipaisAutoresAsync();

            return SucessoLista(objetos.Select(usuarioMapper.GetDto));
        }
        catch
        {
            return FalhaLista<UsuarioDTO>([new("Erro na tentativa de retornar os principais autores.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoUnico<UsuarioDTO>> ObterUsuarioPorEmail(string email)
    {
        try
        {
            var obj = await usuarioDAO.RetornarPorEmailAsync(email);

            if (obj == null)
                return FalhaObjeto<UsuarioDTO>([new("O usuário que você deseja não foi encontrado no sistema.")]);

            return SucessoObjeto(usuarioMapper.GetDto(obj));
        }
        catch
        {
            return FalhaObjeto<UsuarioDTO>([new("Erro na tentativa de obter usuário por e-mail.", MensagemRetorno.EOrigem.Erro)]);
        }
    }


    public async Task<ResultadoUnico<UsuarioDTO>> ObterUsuarioPorSlug(string slug)
    {
        try
        {
            var obj = await usuarioDAO.RetornarPorSlugAsync(slug);

            if (obj == null)
                return FalhaObjeto<UsuarioDTO>([new("O usuário que você deseja não foi encontrado no sistema.")]);

            return SucessoObjeto(usuarioMapper.GetDto(obj));
        }
        catch
        {
            return FalhaObjeto<UsuarioDTO>([new("Erro na tentativa de obter usuário por slug.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoUnico<UsuarioDTO>> LogarAsync(LoginDTO login)
    {
        try
        {
            var usuario = await usuarioDAO.RetornarPorEmailAsync(login.Email);

            if (usuario == null)
                return FalhaObjeto<UsuarioDTO>([new("Usuário e/ou senha inválidos!")]);

            if (hasher.VerifyHashedPassword(usuario, usuario.HashSenha, login.Senha) == PasswordVerificationResult.Failed)
                return FalhaObjeto<UsuarioDTO>([new("Usuário e/ou senha inválidos!")]);

            return SucessoObjeto(usuarioMapper.GetDto(usuario));
        }
        catch
        {
            return FalhaObjeto<UsuarioDTO>([new("Erro no login.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoVoid> TrocarSenhaAsync(TrocarSenhaDTO trocarSenha)
    {
        try
        {
            var usuario = await usuarioDAO.RetornarPorEmailAsync(trocarSenha.Email);

            if (usuario == null)
                return Falha([new("Usuário e/ou senha inválidos!")]);

            if (!usuario.HashSenha.Equals("") && hasher.VerifyHashedPassword(usuario, usuario.HashSenha, trocarSenha.SenhaAntiga) == PasswordVerificationResult.Failed)
            {
                return Falha([new("Usuário e/ou senha inválidos!")]);
            }

            usuario.HashSenha = hasher.HashPassword(usuario, trocarSenha.NovaSenha);

            await usuarioDAO.AlterarAsync(usuario);

            return Sucesso();
        }
        catch
        {
            return Falha([new("Erro no login.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoUnico<UsuarioDTO>> InserirUsuario(UsuarioDTO usuario)
    {
        try
        {
            var obj = new Usuario();
            usuarioMapper.PreencherModel(obj, usuario);
            //usuario.Id = é preciso gerar novo id aqui...
            await usuarioDAO.InserirAsync(obj);

            return SucessoObjeto(usuarioMapper.GetDto(obj));
        }
        catch
        {
            return FalhaObjeto<UsuarioDTO>([new("Erro na tentativa de inserir novo usuário.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<bool> SlugJaUtilizadoAsync(string slug, int idUsuarioAtual)
    {
        try
        {
            return await usuarioDAO.SlugJaUtilizadoAsync(slug, idUsuarioAtual);
        }
        catch
        {
            return false;
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

    public async Task<ResultadoUnico<UsuarioDTO>> ObterUsuarioPorId(long id)
    {
        if (idUsuarioLogado != id)
            return FalhaObjeto<UsuarioDTO>([new("Acesso não permitido.")]);

        try
        {
            var obj = await usuarioDAO.RetornarPorIdAsync(id);

            if (obj == null)
                return FalhaObjeto<UsuarioDTO>([new("O usuário que você deseja não foi encontrado no sistema.")]);

            return SucessoObjeto(usuarioMapper.GetDto(obj));
        }
        catch
        {
            return FalhaObjeto<UsuarioDTO>([new("Erro na tentativa de obter usuário por id.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    #endregion
}
