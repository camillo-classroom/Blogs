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

    public async Task<ResultadoLista<PostagemDTO>> ConsultarPostagensAsync(long idAutor, long ultimoIdPostagemConsultado)
    {
        try
        {
            var obj = await postagemDAO.RetornarComPaginacaoDescendenteAsync(idAutor, ultimoIdPostagemConsultado);

            if (obj == null)
                return FalhaLista<PostagemDTO>([new("Nenhuma postagem existe no sistema.")]);

            var objetos = await postagemDAO.RetornarComPaginacaoDescendenteAsync(idAutor, ultimoIdPostagemConsultado);
            return SucessoLista(objetos.Select(x => postagemMapper.GetDto(x)));
        }
        catch
        {
            return FalhaLista<PostagemDTO>([new("Erro na tentativa de consultar postagens.", MensagemRetorno.EOrigem.Erro)]);
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
                return Falha([new("A postagem que você deseja alterar não foi encontrada no sistema.")]);

            if (obj.IdAutor != idUsuarioLogado)
                return Falha([new("Acesso não permitido.")]);

            postagemMapper.PreencherModel(obj, postagem);
            await postagemDAO.AlterarAsync(obj);

            return Sucesso();
        }
        catch
        {
            return Falha([new("Erro na tentativa de alterar postagem.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoVoid> ExcluirPostagem(long id)
    {
        if (idUsuarioLogado == 0)
            return Falha([new("Acesso não permitido.")]);

        try
        {
            var obj = await postagemDAO.RetornarPorIdAsync(id);

            if (obj == null)
                return Falha([new("A postagem que você deseja excluir não foi encontrada no sistema.")]);

            if (obj.IdAutor != idUsuarioLogado)
                return Falha([new("Acesso não permitido.")]);

            await postagemDAO.Ocultar(obj.Id);
            return Sucesso();
        }
        catch
        {
            return Falha([new("Erro na tentativa de excluir postagem.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    public async Task<ResultadoUnico<PostagemDTO>> InserirPostagem(PostagemDTO postagem)
    {
        if (idUsuarioLogado == 0)
            return FalhaObjeto<PostagemDTO>([new("Acesso não permitido.")]);

        try
        {
            var obj = new Postagem();
            obj.IdAutor = idUsuarioLogado;
            
            postagemMapper.PreencherModel(obj, postagem);
            await postagemDAO.InserirAsync(obj);
            return SucessoObjeto(postagemMapper.GetDto(obj));
        }
        catch
        {
            return FalhaObjeto<PostagemDTO>([new("Erro na tentativa de alterar postagem.", MensagemRetorno.EOrigem.Erro)]);
        }
    }

    #endregion
}
