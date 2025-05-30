using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blogs.UseCases.MensagemRetorno;

namespace Blogs.UseCases
{
    public record MensagemRetorno(string Mensagem, EOrigem Origem = EOrigem.RegraDeNegocio)
    {
        public enum EOrigem
        {
            RegraDeNegocio = 0,
            Erro = 1
        }
    }
}
