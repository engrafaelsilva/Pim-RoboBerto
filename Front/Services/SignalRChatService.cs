using Microsoft.AspNetCore.SignalR.Client;

public class SignalRChatService
{
    private HubConnection? _connection;

    public bool Conectado => _connection?.State == HubConnectionState.Connected;

    public event Action<Guid, MensagemDTO>? OnMensagemRecebida;

    public async Task IniciarAsync(string baseUrl, string token)
    {
        // Garante que a URL nunca termina com "/"
        var trimmedBase = baseUrl.TrimEnd('/');

        // NÃO adiciona "Bearer " aqui, vai só o JWT cru
        var jwt = token ?? string.Empty;

        if (_connection != null && _connection.State != HubConnectionState.Disconnected)
            return;

        _connection = new HubConnectionBuilder()
            .WithUrl(new Uri(new Uri(trimmedBase), "hubs/chat"), options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(jwt);
            })
            .WithAutomaticReconnect()
            .Build();

        // Callback que o backend chama: Clients.Group().ReceberMensagem(...)
        _connection.On<Guid, MensagemDTO>("ReceberMensagem", (chamadoId, msg) =>
        {
            OnMensagemRecebida?.Invoke(chamadoId, msg);
        });

        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            // Deixa a Exception subir, o caller já está tratando com alerta
            throw new InvalidOperationException(
                $"Erro ao conectar no hub SignalR em {trimmedBase}/hubs/chat: {ex.Message}", ex);
        }
    }

    public async Task EntrarNoChamado(Guid chamadoId)
    {
        if (_connection == null) return;
        await _connection.InvokeAsync("EntrarNoGrupoDoChamado", chamadoId);
    }

    public async Task SairDoChamado(Guid chamadoId)
    {
        if (_connection == null) return;
        await _connection.InvokeAsync("SairDoGrupoDoChamado", chamadoId);
    }

    public async Task EnviarMensagem(Guid chamadoId, string mensagem)
    {
        if (_connection == null) return;
        await _connection.InvokeAsync("EnviarMensagemParaGrupo", chamadoId, mensagem);
    }

    public async Task EncerrarAsync()
    {
        if (_connection != null)
        {
            await _connection.StopAsync();
            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}

public class MensagemDTO
{
    public Guid Id { get; set; }
    public string Texto { get; set; } = "";
    public DateTime DataHoraMensagem { get; set; }
}
