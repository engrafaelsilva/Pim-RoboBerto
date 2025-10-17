using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IDepartamentoServico
    {
        Task <PagedList<DepartamentoDTO>> ListarTodosDepComUsuariosAsync(int paginaAtual, int tamanho);
    }
}
