using IA_RoboBerto.DTOs;

namespace IA_RoboBerto.Hub
{
    public interface IChatCliente
    {
        Task ReceberMensagem(Guid chamadoId, MensagemDTO mensagem);
    }
}
