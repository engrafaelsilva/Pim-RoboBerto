using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IChamadoServico
    {
        Task<PagedList<ChamadoDTO>> ListarTodosAsync(int paginaAtual, int tamanho);
    }
}
