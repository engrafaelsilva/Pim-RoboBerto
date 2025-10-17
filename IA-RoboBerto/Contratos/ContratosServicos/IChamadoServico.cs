using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IChamadoServico
    {
        PagedList<ChamadoDTO> ListarTodos(int paginaAtual, int tamanho);
    }
}
