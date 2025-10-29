using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IMensagensServico
    {
        Task<MensagemDTO> ComentarAsync(Chamado chamado, MensagemInsertDTO dto);
    }
}
