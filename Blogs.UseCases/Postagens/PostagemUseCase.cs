using Blogs.DTO.Postagens;
using Blogs.Infra.Postagens;
using Blogs.Mappers;
using Blogs.Model.Postagens;

namespace Blogs.UseCases.Postagens;

public class PostagemUseCase
(
    IPostagemDAO postagemDAO,
    IMapper<Postagem, PostagemDTO> postagemMapper
): BaseUseCase, IPostagemUseCase
{
    #region Todos_usuarios

    public async Task<ResultadoLista<PostagemDTO>> ConsultarPostagensAsync(long ultimoIdConsultado)
    {
        try
        {
            var obj = await postagemDAO.RetornarComPaginacaoDescendenteAsync(ultimoIdConsultado);

            if (obj == null)
                return new (Sucesso: false, Objetos: null, Erros: [new("Nenhuma postagem existe no sistema.")]);

            var objetos = await postagemDAO.RetornarComPaginacaoDescendenteAsync(ultimoIdConsultado);
            return new(Sucesso: true, Objetos: objetos.Select(x => postagemMapper.GetDto(x)), Erros: null);
        }
        catch
        {
            return new(Sucesso: false, Objetos: null, Erros: [new("Erro na tentativa de consultar postagens.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    #endregion

    #region Apenas_logados

    public async Task<ResultadoVoid> AlterarPostagem(PostagemDTO postagem)
    {
        if (idUsuarioLogado == 0)
            return new (Sucesso: false, Erros: [new("Acesso não permitido.")]);
        
        try
        {
            var obj = await postagemDAO.RetornarPorIdAsync(postagem.Id);

            if (obj == null)
                return new(Sucesso: false, Erros: [new("A postagem que você deseja alterar não foi encontrada no sistema.")]);

            if (obj.IdAutor != idUsuarioLogado)
                return new(Sucesso: false, Erros: [new("Acesso não permitido.")]);

            postagemMapper.PreencherModel(obj, postagem);
            await postagemDAO.AlterarAsync(obj);

            return new(Sucesso: true, Erros: null);
        }
        catch
        {
            return new(Sucesso: false, Erros: [new("Erro na tentativa de alterar postagem.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoVoid> ExcluirPostagem(PostagemDTO postagem)
    {
        if (idUsuarioLogado == 0)
            return new(Sucesso: false, Erros: [new("Acesso não permitido.")]);

        try
        {
            var obj = await postagemDAO.RetornarPorIdAsync(postagem.Id);

            if (obj == null)
                return new(Sucesso: false, Erros: [new("A postagem que você deseja excluir não foi encontrada no sistema.")]);

            if (obj.IdAutor != idUsuarioLogado)
                return new(Sucesso: false, Erros: [new("Acesso não permitido.")]);

            await postagemDAO.Ocultar(obj.Id);
            return new(Sucesso: true, Erros: null);
        }
        catch
        {
            return new(Sucesso: false, Erros: [new("Erro na tentativa de excluir postagem.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoVoid> InserirPostagem(PostagemDTO postagem)
    {
        if (idUsuarioLogado == 0)
            return new(Sucesso: false, Erros: [new("Acesso não permitido.")]);

        try
        {
            var obj = new Postagem();
            //obj.Id = ... precisa preencher o id
            obj.IdAutor = idUsuarioLogado;
            
            postagemMapper.PreencherModel(obj, postagem);
            await postagemDAO.InserirAsync(obj);
            return new(Sucesso: true, Erros: null);
        }
        catch
        {
            return new(Sucesso: false, Erros: [new("Erro na tentativa de alterar postagem.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    #endregion
}
