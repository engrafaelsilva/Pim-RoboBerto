using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<PagedList<Usuario>> ListarTodosAsync(int paginaAtual, int tamanho);
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<Usuario?> ObterPorIdAsync(Guid id);
        Task<Usuario> AdicionarAsync(Usuario usuario);
        Task<Usuario?> AtualizarAsync(Usuario usuario);
        Task<Usuario?> ObterPorNomeAsync(string nome);
        Task<bool> RemoverAsync(Guid id);
        Task<bool> IdExisteAsync(Guid id);
    }
}
