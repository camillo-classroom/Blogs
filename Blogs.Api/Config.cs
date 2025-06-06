using System.Text.Json;

namespace Blogs.Api;

public class Config
{
    static Config()
    {
        var caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.secrets.json");
        if (File.Exists(caminho))
            instancia = JsonSerializer.Deserialize<Config>(File.ReadAllText(caminho));
        else
            throw new Exception("Configuração da chave privada inexistente");
    }

    public string ChavePrivada { get; set; } = "";
    public static Config Instancia => instancia ?? new Config();
    private static readonly Config? instancia = null;
}
