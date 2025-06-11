using System.Diagnostics;
using System.Reflection;
using Blogs.DTO.Postagens;
using Blogs.UseCases.Postagens;

namespace Blogs.Api.Endpoints
{
    public static class AdicionarEndpointsPostagensReacoesExtensions
    {
        /// <summary>
        /// Método para adicionar os endpoints de usuários da API.
        /// </summary>
        /// <param name="app">Instância do WebApplication.</param>
        public static void AdicionarEndpointsPostagensReacoes(this RouteGroupBuilder group)
        {
            group.MapPost("/{idPostagem}/like", Like)
                .WithName("Like em postagem")
                .WithSummary("Dá like na postagem para o usuário logado")
                .RequireAuthorization();

            group.MapPost("/{idPostagem}/deslike", Deslike)
                .WithName("Deslike em postagem")
                .WithSummary("Dá deslike na postagem para o usuário logado")
                .RequireAuthorization();
        }

        private static async Task<IResult> Like(long idPostagem, IPostagemReacaoUseCase postagemReacaoUseCase, HttpContext context)
        {
            try
            {
                postagemReacaoUseCase.IdentificarAcesso(context.User.GetId());

                var resultado = await postagemReacaoUseCase.Like(idPostagem);
                
                return resultado.Sucesso
                    ? TypedResults.Ok()
                    : TypedResults.BadRequest(resultado.Erros);
            }
            catch (Exception ex)
            {
#if DEBUG
                var metodo = MethodBase.GetCurrentMethod();

                if (metodo != null)
                    Debug.WriteLine($"Exception in {metodo.Name}: {ex.Message}");
#endif

                return TypedResults.InternalServerError();
            }
        }

        private static async Task<IResult> Deslike(long idPostagem, IPostagemReacaoUseCase postagemReacaoUseCase, HttpContext context)
        {
            try
            {
                postagemReacaoUseCase.IdentificarAcesso(context.User.GetId());

                var resultado = await postagemReacaoUseCase.Deslike(idPostagem);

                return resultado.Sucesso
                    ? TypedResults.Ok()
                    : TypedResults.BadRequest(resultado.Erros);
            }
            catch (Exception ex)
            {
#if DEBUG
                var metodo = MethodBase.GetCurrentMethod();

                if (metodo != null)
                    Debug.WriteLine($"Exception in {metodo.Name}: {ex.Message}");
#endif

                return TypedResults.InternalServerError();
            }
        }
    }
}
