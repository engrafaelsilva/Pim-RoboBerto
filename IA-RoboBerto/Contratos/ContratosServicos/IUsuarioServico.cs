using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IUsuarioServico
    {
        Task<PagedList<UsuarioMaxDTO>> ListarTodosAsync(int paginaAtual, int tamanho);
     
        Task<UsuarioInsertDTO> AdicionarAsync(UsuarioInsertDTO dto);
        Task<UsuarioMaxDTO?> AtualizarAsync(Guid id, UsuarioMaxDTO dto);
        Task<UsuarioMaxDTO?> ObterPorIdAsync(Guid id);
        Task<UsuarioMaxDTO?> ObterPorNomeAsync(string nome);
        Task<UsuarioMaxDTO?> ObterPorEmailAsync(string email);
        Task<Usuario?> ValidarUsuarioAsync(string email, string senha);
        Task<bool> RemoverAsync(Guid id);
    }
}
