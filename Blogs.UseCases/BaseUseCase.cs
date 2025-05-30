using Blogs.Model.ControleAcessos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blogs.UseCases
{
    public class BaseUseCase
    {
        public void IdentificarAcesso(long idUsuario)
        {
            this.idUsuarioLogado = idUsuario;
        }

        public static ResultadoVoid Sucesso() =>
            new (Sucesso: false, Erros: null);

        public static ResultadoVoid Falha(IEnumerable<MensagemRetorno> erros) =>
            new(Sucesso: false, Erros: erros);

        public static ResultadoUnico<T> SucessoObjeto<T>(T objeto) =>
            new(Sucesso: false, Objeto: objeto, Erros: null);

        public static ResultadoUnico<T> FalhaObjeto<T>(IEnumerable<MensagemRetorno> erros) =>
            new(Sucesso: false, Objeto: default, Erros: erros);

        public static ResultadoLista<T> SucessoLista<T>(IEnumerable<T> objetos) =>
            new(Sucesso: false, Objetos: objetos, Erros: null);

        public static ResultadoLista<T> FalhaLista<T>(IEnumerable<MensagemRetorno> erros) =>
            new(Sucesso: false, Objetos: default, Erros: erros);

        protected long idUsuarioLogado;
    }
}
