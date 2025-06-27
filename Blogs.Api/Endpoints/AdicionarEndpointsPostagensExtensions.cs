using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Blogs.DTO.Postagens;
using Blogs.Infra.ControlesAcessos;
using Blogs.UseCases.ControleAcessos;
using Blogs.UseCases.Postagens;
using Ganss.Xss;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Win32.SafeHandles;

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
            var postagens = app.MapGroup("/postagens")
                .WithTags("Postagens")
                .WithDescription("Endpoints relacionados a postagens");

            postagens.MapGet("/{idAutor}/{idUltimaPostagem?}", RetornarPostagens)
                .WithName("Retornar postagens")
                .WithSummary("Retorna postagens com id anteriores ao da última postagem, quando este id é informado");

            postagens.MapPost("/", InserirPostagem)
                .WithName("Inserir postagem")
                .WithSummary("Insere uma nova postagem no sistema")
                .RequireAuthorization();

            postagens.MapPut("/{id}", AlterarPostagem)
                .WithName("Alterar postagem")
                .WithSummary("Altera uma postagem no sistema")
                .RequireAuthorization();

            postagens.MapDelete("/{id}", ExcluirPostagem)
                .WithName("Excluir postagem")
                .WithSummary("Exclui uma postagem no sistema")
                .RequireAuthorization();

            postagens.AdicionarEndpointsPostagensReacoes();
        }

        private static async Task<IResult> RetornarPostagens(long idAutor, long? idUltimaPostagem, IPostagemUseCase postagemUseCase, IDistributedCache cache)
        {
            string cacheKey = $"{nameof(RetornarPostagens)}/{idAutor}/{idUltimaPostagem}";
            
            try
            {
                var objetosEmCache = await cache.GetAsync(cacheKey);

                if (objetosEmCache != null)
                    return TypedResults.Content(Encoding.UTF8.GetString(objetosEmCache), "application/json");
                
                var resultado = await postagemUseCase.ConsultarPostagensAsync(idAutor, idUltimaPostagem);

                return resultado.Sucesso ?
                    TypedResults.Ok(await cache.SetCacheAndReturnObjectAsync(cacheKey, resultado.Objetos)) :
                    TypedResults.BadRequest(resultado.Erros);
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

        private static async Task<IResult> InserirPostagem(PostagemDTO obj, HttpContext context, IPostagemUseCase postagemUseCase)
        {
            try
            {
                obj.Conteudo = GetSanitizer().Sanitize(obj.Conteudo);

                obj.DataHora = DateTime.Now;

                postagemUseCase.IdentificarAcesso(context.User.GetId());

                var resultado = await postagemUseCase.InserirPostagem(obj);

                return resultado.Sucesso
                    ? TypedResults.Created($"/{resultado.Objeto.Id}", resultado.Objeto)
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

                obj.Conteudo = GetSanitizer().Sanitize(obj.Conteudo);

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

        private static HtmlSanitizer sanitizer;

        private static HtmlSanitizer GetSanitizer()
        {
            if (sanitizer == null)
            {
                sanitizer = new HtmlSanitizer();
                sanitizer.AllowedTags.Clear();
                sanitizer.AllowedAtRules.Clear();
                sanitizer.AllowedClasses.Clear();
                sanitizer.AllowedCssProperties.Clear();
                sanitizer.AllowedSchemes.Clear();
                sanitizer.AllowedTags.Add("br");
            }

            return sanitizer;
        }
    }
}
