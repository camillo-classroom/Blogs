
using Blogs.DTO.ControleAcessos;
using Blogs.DTO.Postagens;
using Blogs.Infra.ControlesAcessos;
using Blogs.Infra.Postagens;
using Blogs.Mappers;
using Blogs.Mappers.ControleAcessos;
using Blogs.Mappers.Postagens;
using Blogs.Model.ControleAcessos;
using Blogs.Model.Postagens;
using Blogs.UseCases.ControleAcessos;
using Blogs.UseCases.Postagens;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Api
{
    public static class InjecoesDependencias
    {
        internal static void InjetarDependencias(this IServiceCollection services)
        {
            #region DAOs
            
            services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            services.AddScoped<IUsuarioDAO, UsuarioDAO>();
            services.AddScoped<IPostagemDAO, PostagemDAO>();
            services.AddScoped<IPostagemReacaoDAO, PostagemReacaoDAO>();

            #endregion

            #region UseCases

            services.AddScoped<IControleAcessoUseCase, ControleAcessoUseCase>();
            services.AddScoped<IPostagemUseCase, PostagemUseCase>();
            services.AddScoped<IPostagemReacaoUseCase, PostagemReacaoUseCase>();

            #endregion

            #region Mappers

            services.AddScoped<IMapper<Usuario, UsuarioDTO>, UsuarioMapper>();
            services.AddScoped<IMapper<Postagem, PostagemDTO>, PostagemMapper>();
            services.AddScoped<IMapper<PostagemReacao, PostagemReacaoDTO>, PostagemReacaoMapper>();

            #endregion
        }
    }
}
