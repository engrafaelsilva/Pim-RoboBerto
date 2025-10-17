using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IDepartamentoServico
    {
     
        Task<PagedList<DepartamentoDTO>> ListarTodosDepComUsuariosAsync(int paginaAtual, int tamanho);

        Task<DepartamentoDTO?> ObterPorIdAsync(Guid id);

        Task<DepartamentoDTO?> ObterPorNomeAsync(string nome);

        Task<DepartamentoDTO> AdicionarAsync(DepartamentoDTO dto);

        Task<DepartamentoDTO?> AtualizarAsync(Guid id, DepartamentoDTO dto);

        Task<bool> RemoverAsync(Guid id);
    }
}
