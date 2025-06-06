using System.Diagnostics;
using System.Reflection;
using Blogs.DTO.Postagens;
using Blogs.UseCases.Postagens;

namespace Blogs.Api.Endpoints
{
    public static class AdicionarEndpointsPostagensExtensions
    {
        /// <summary>
        /// Método para adicionar os endpoints de usuários da API.
        /// </summary>
        /// <param name="app">Instância do WebApplication.</param>
        public static void AdicionarEndpointsPostagens(this WebApplication app)
        {
            var usuarios = app.MapGroup("/postagens")
                .WithTags("Postagens")
                .WithDescription("Endpoints relacionados a postagens");

            usuarios.MapGet("/{idAutor}/{:idUltimaPostagem}", InserirPostagem)
                .WithName("Inserir postagem")
                .WithSummary("Insere uma nova postagem no sistema")
                .RequireAuthorization();

            usuarios.MapPost("/", InserirPostagem)
                .WithName("Inserir postagem")
                .WithSummary("Insere uma nova postagem no sistema")
                .RequireAuthorization();

            usuarios.MapPut("/{id}", AlterarPostagem)
                .WithName("Alterar postagem")
                .WithSummary("Altera uma postagem no sistema")
                .RequireAuthorization();

            usuarios.MapDelete("/{id}", ExcluirPostagem)
                .WithName("Excluir postagem")
                .WithSummary("Exclui uma postagem no sistema")
                .RequireAuthorization();
        }

        private static async Task<IResult> InserirPostagem(PostagemDTO obj, HttpContext context, IPostagemUseCase postagemUseCase)
        {
            try
            {
                postagemUseCase.IdentificarAcesso(context.User.GetId());

                var resultado = await postagemUseCase.InserirPostagem(obj);

                return resultado.Sucesso
                    ? TypedResults.Created($"/{resultado.Objeto?.Id}", resultado.Objeto)
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

        private static async Task<IResult> AlterarPostagem(long id, PostagemDTO obj, HttpContext context, IPostagemUseCase postagemUseCase)
        {
            try
            {
                postagemUseCase.IdentificarAcesso(context.User.GetId());

                if (id != obj.Id)
                    return TypedResults.BadRequest("O id não confere.");

                var resultado = await postagemUseCase.AlterarPostagem(obj);
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

        private static async Task<IResult> ExcluirPostagem(long id, HttpContext context, IPostagemUseCase postagemUseCase)
        {
            try
            {
                postagemUseCase.IdentificarAcesso(context.User.GetId());

                var resultado = await postagemUseCase.ExcluirPostagem(id);
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
