using Blogs.Infra;
using Dapper;
using Microsoft.Data.Sqlite;

public abstract class BaseDAO<T> : IBaseDAO<T> where T : IModel
{
    protected abstract string NomeTabela { get; }

    public virtual void Inserir(T obj)
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
        
        Executar(sql, obj);
    }

    public virtual void Alterar(T obj)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(obj))
            campos += $"{nomeProp.ToLower()} = @{nomeProp}";

        string sql = $"UPDATE {NomeTabela}" +
            $" SET {campos}" +
            " WHERE " +
            " id = @Id";

        Executar(sql, obj);
    }

    public virtual void Excluir(long id)
    {
        string sql = $"DELETE {NomeTabela}" +
            " WHERE " +
            " id = @Id";

        Executar(sql, new { Id = id });
    }

    public virtual IList<T> RetornarTodos()
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(typeof(T)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT id as Id{campos}" + 
            $" FROM {NomeTabela}";

        return Selecionar(sql);
    }

    public virtual T? RetornarPorId(long id)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(typeof(T)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT id as Id{campos}" + 
            $" FROM {NomeTabela}" +
            " WHERE id = @id";

        return SelecionarUnico(sql, new { id });
    }

    private void Executar(string sql, object obj)
    {
        using var conexao = new SqliteConnection("Data Source=db/app.db");

        conexao.Open();

        conexao.Execute(sql, obj); 
    }

    protected IList<T> Selecionar(string sql, object? obj = null)
    {
        using var conexao = new SqliteConnection("Data Source=db/app.db");

        conexao.Open();

        if (obj == null)
            return conexao.Query<T>(sql).ToList();

        return conexao.Query<T>(sql, obj).ToList();
    }

    protected T? SelecionarUnico(string sql, object? obj = null)
    {
        using var conexao = new SqliteConnection("Data Source=db/app.db");

        conexao.Open();

        if (obj == null)
            return conexao.QuerySingle<T>(sql);

        return conexao.QuerySingle<T>(sql, obj);
    }

    private IEnumerable<string> GetPropriedades(T obj)
    {
        return GetPropriedades(obj.GetType());
    }

    private IEnumerable<string> GetPropriedades(Type tipo)
    {
        //LINQ - Language Integrated Query
        return tipo.GetProperties().Where(x => !x.Name.Equals("Id")).Select(x => x.Name);
    }
}