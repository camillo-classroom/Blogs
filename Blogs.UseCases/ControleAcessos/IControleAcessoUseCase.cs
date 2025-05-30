using Blogs.DTO.ControleAcessos;
using Blogs.Infra.ControlesAcessos;
using Blogs.Mappers.ControleAcessos;
using Blogs.Model.ControleAcessos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.UseCases.ControleAcessos;

public interface IControleAcessoUseCase
{
    #region Todos_Usuarios

    bool Logar(LoginDTO login, out IEnumerable<MensagemRetorno>? erros);
    bool TrocarSenha(TrocarSenhaDTO trocarSenha, out IEnumerable<MensagemRetorno>? erros);
    bool InserirUsuario(UsuarioDTO usuario, out IEnumerable<MensagemRetorno>? erros);
    bool AlterarUsuario(UsuarioDTO usuario, out IEnumerable<MensagemRetorno>? erros);

    #endregion
}
