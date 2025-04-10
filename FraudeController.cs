// Controllers/FraudeController.cs
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using quod_backend_api.Models;

[ApiController]
[Route("api/[controller]")]
public class FraudeController : ControllerBase
{
    private readonly IMongoDatabase _database;

    public FraudeController(IMongoDatabase database)
    {
        _database = database;
    }

    [HttpPost("notificacoes/fraude")]
    public async Task<IActionResult> NotificarFraude([FromBody] FraudNotification request)
    {
        // Validação básica
        if (string.IsNullOrEmpty(request.TransacaoId)) 
        return BadRequest("TransacaoId é obrigatório.");

        // Simular detecção de fraude (exemplo simplificado)
        bool isFraude = request.TipoFraude != "nenhuma";

        // Persistir no MongoDB
        var collection = _database.GetCollection<FraudNotification>("Fraudes");
        await collection.InsertOneAsync(request);

        // Notificar se for fraude (simulação)
        if (isFraude)
        {
            await SimularNotificacao(request);
        }

        return Ok(new { Status = isFraude ? "Fraude detectada" : "Sem fraude" });
    }

    private async Task SimularNotificacao(FraudNotification request)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsJsonAsync(
            "https://api.quod.com.br/monitoramento",
            new { request.TransacaoId, request.TipoFraude }
        );
    }
}