using Blogs.DTO.ControleAcessos;

namespace Blogs.UseCases.ControleAcessos;

public interface IControleAcessoUseCase
{
    void IdentificarAcesso(long idUsuario);

    #region Todos_Usuarios

    Task<ResultadoUnico<UsuarioDTO>> LogarAsync(LoginDTO login);

    Task<ResultadoVoid> TrocarSenhaAsync(TrocarSenhaDTO trocarSenha);

    Task<ResultadoUnico<UsuarioDTO>> InserirUsuario(UsuarioDTO usuario);

    #endregion

    #region Apenas_Logados

    Task<ResultadoVoid> AlterarUsuario(UsuarioDTO usuario);
    Task<bool> SlugJaUtilizadoAsync(string slug, int v);
    Task<ResultadoUnico<UsuarioDTO>> ObterUsuarioPorId(long id);

    #endregion
}
