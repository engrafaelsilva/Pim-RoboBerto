using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IUsuarioServico
    {
        Task<PagedList<UsuarioMaxDTO>> ListarTodosAsync(int paginaAtual, int tamanho);
        Task<UsuarioMaxDTO?> ObterPorIdAsync(Guid id);
        Task<UsuarioInsertDTO> AdicionarAsync(UsuarioInsertDTO dto);
        Task<UsuarioMaxDTO?> AtualizarAsync(Guid id, UsuarioMaxDTO dto);
        Task<bool> RemoverAsync(Guid id);
    }
}
