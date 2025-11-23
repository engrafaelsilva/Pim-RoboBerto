// IA_RoboBerto.Hub/ChatHub.cs
using IA_RoboBerto.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace IA_RoboBerto.Hub
{
    [Authorize]
    public class ChatHub : Hub<IChatCliente>
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Conexão: {Context.ConnectionId} (usuário: {Context.UserIdentifier})");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Desconectado: {Context.ConnectionId} (usuário: {Context.UserIdentifier})");
            return base.OnDisconnectedAsync(exception);
        }

        // Recebe um GUID (tipo Guid) — binder do SignalR converte automaticamente
        public Task EntrarNoGrupoDoChamado(Guid chamadoId)
        {
            var grupo = ObterNomeDoGrupo(chamadoId);
            Console.WriteLine($"Adicionar {Context.ConnectionId} ao grupo {grupo} (user {Context.UserIdentifier})");
            return Groups.AddToGroupAsync(Context.ConnectionId, grupo);
        }

        public Task SairDoGrupoDoChamado(Guid chamadoId)
        {
            var grupo = ObterNomeDoGrupo(chamadoId);
            Console.WriteLine($"Remover {Context.ConnectionId} do grupo {grupo} (user {Context.UserIdentifier})");
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, grupo);
        }
        public async Task EnviarMensagemParaGrupo(Guid chamadoId, string texto)
        {
            var mensagemDto = new MensagemDTO
            {
                Id = Guid.NewGuid(),
                Texto = texto,
                DataHoraMensagem = DateTime.UtcNow
            };

            await Clients.Group(ObterNomeDoGrupo(chamadoId)).ReceberMensagem(chamadoId, mensagemDto);
        }
        private static string ObterNomeDoGrupo(Guid chamadoId) => $"chamado-{chamadoId}";
    }
}
