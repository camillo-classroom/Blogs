using Blogs.Model;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Blogs.Infra;

public abstract class BaseDAO<T> : IBaseDAO<T> where T : IModel
{
    protected abstract string NomeTabela { get; }

    public virtual async Task InserirAsync(T obj)
    {
        string nomesCampos = "";
        string parametrosCampos = "";

        foreach (var nomeProp in GetPropriedades(obj))
        {
            if (!nomeProp.ToLower().Equals("id"))
            {
                nomesCampos += $", {nomeProp.ToLower()}";
                parametrosCampos += $", @{nomeProp}";
            }
        }

        string sql = $"INSERT INTO {NomeTabela}" +
            $" (id{nomesCampos})" +
            " values " +
            $" (@Id{parametrosCampos})";
        
        await ExecutarAsync(sql, obj);
    }

    public virtual async Task AlterarAsync(T obj)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(obj))
            campos += $"{nomeProp.ToLower()} = @{nomeProp}";

        string sql = $"UPDATE {NomeTabela}" +
            $" SET {campos}" +
            " WHERE " +
            " id = @Id";

        await ExecutarAsync(sql, obj);
    }

    public virtual async Task ExcluirAsync(long id)
    {
        string sql = $"DELETE {NomeTabela}" +
            " WHERE " +
            " id = @Id";

        await ExecutarAsync(sql, new { Id = id });
    }

    public virtual async Task<IEnumerable<T>> RetornarComPaginacaoDescendenteAsync(long ultimoIdConsultado, int numeroRegsASeremRetornados)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(typeof(T)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT TOP {numeroRegsASeremRetornados} id as Id{campos}" + 
            $" FROM {NomeTabela} WHERE id < @Id ORDER BY id DESC";

        return await SelecionarAsync(sql, new { Id = ultimoIdConsultado });
    }

    public virtual async Task<T?> RetornarPorIdAsync(long id)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(typeof(T)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT id as Id{campos}" + 
            $" FROM {NomeTabela}" +
            " WHERE id = @id";

        return await SelecionarUnicoAsync(sql, new { id });
    }

    protected async Task ExecutarAsync(string sql, object obj)
    {
        using var conexao = new SqliteConnection("Data Source=db/app.db");

        await conexao.OpenAsync();

        await conexao.ExecuteAsync(sql, obj);
        
        await conexao.OpenAsync();
    }

    protected async Task<IEnumerable<T>> SelecionarAsync(string sql, object? obj = null)
    {
        using var conexao = new SqliteConnection("Data Source=db/app.db");

        await conexao.OpenAsync();

        if (obj == null)
            return await conexao.QueryAsync<T>(sql);

        return await conexao.QueryAsync<T>(sql, obj);
    }

    protected async Task<T?> SelecionarUnicoAsync(string sql, object? obj = null)
    {
        using var conexao = new SqliteConnection("Data Source=db/app.db");

        await conexao.OpenAsync();

        if (obj == null)
            return await conexao.QuerySingleAsync<T>(sql);

        return await conexao.QuerySingleAsync<T>(sql, obj);
    }

    protected IEnumerable<string> GetPropriedades(T obj)
    {
        return GetPropriedades(obj.GetType());
    }

    protected IEnumerable<string> GetPropriedades(Type tipo)
    {
        //LINQ - Language Integrated Query
        return tipo.GetProperties().Where(x => !x.Name.Equals("Id")).Select(x => x.Name);
    }
}