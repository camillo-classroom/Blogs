public static class AdicionarEnpointsUsuarios
{
    /// <summary>
    /// Método para adicionar os endpoints de usuários da API.
    /// </summary>
    /// <param name="app">Instância do WebApplication.</param>
    public static void AdicionarEndpointsUsuarios(this WebApplication app)
    {
        var usuarios = app.MapGroup("/usuarios")
            .WithTags("Usuários")
            .WithDescription("Endpoints relacionados a usuários");

        usuarios.MapGet("/{id}", (int id) => $"Detalhes do usuário com ID {id}")
            .WithName("ObterUsuarioPorId")
            .WithSummary("Obtém os detalhes de um usuário específico pelo ID");
            
        
    }
}