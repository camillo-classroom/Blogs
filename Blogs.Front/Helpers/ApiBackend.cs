using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogs.Front.Helpers;

public static class ApiBackend
{
    static ApiBackend()
    {
        UrlBase = "http://localhost:5124/";
    }

    public static async Task<T?> GetAsync<T>(string complementoUrl, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var result = await httpClient.GetStringAsync(urlCompleta);

        httpClient.Dispose();

        return JsonSerializer.Deserialize<T>(result, Options);
    }

    public static async Task<T?> PostAsync<T, K>(string complementoUrl, K objeto, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var conteudo = new StringContent(
            JsonSerializer.Serialize(objeto, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PostAsync(urlCompleta, conteudo);

        if (!result.IsSuccessStatusCode)
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");

        var resultContent = await result.Content.ReadAsStringAsync();

        httpClient.Dispose();

        if (string.IsNullOrEmpty(resultContent))
            return default;

        return JsonSerializer.Deserialize<T>(resultContent, Options);
    }

    public static async Task<T?> PostAsync<T>(string complementoUrl, T objeto, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var conteudo = new StringContent(
            JsonSerializer.Serialize(objeto, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PostAsync(urlCompleta, conteudo);

        if (!result.IsSuccessStatusCode)
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");

        var resultContent = await result.Content.ReadAsStringAsync();

        httpClient.Dispose();

        if (string.IsNullOrEmpty(resultContent))
            return default;

        return JsonSerializer.Deserialize<T>(resultContent, Options);
    }

    public static async Task PutAsync<T>(string complementoUrl, T objeto, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var conteudo = new StringContent(
            JsonSerializer.Serialize(objeto, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PutAsync(urlCompleta, conteudo);

        if (!result.IsSuccessStatusCode)
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");

        httpClient.Dispose();
    }

    public static async Task DeleteAsync(string complementoUrl, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var result = await httpClient.DeleteAsync(urlCompleta);

        if (!result.IsSuccessStatusCode)
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");

        httpClient.Dispose();
    }
    
    private static JsonSerializerOptions Options { get; set; } = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    private static string UrlBase { get; set; } = null!;
}
