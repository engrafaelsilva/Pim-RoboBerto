using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IChamadoRepositorio
    {
        Task<PagedList<Chamado>> ListarTodosAsync(int pagina, int tamanho);
        Task<Chamado?> ObterPorIdAsync(Guid id);
        Task<Chamado> AdicionarAsync(Chamado chamado);
        Task<Chamado?> AtualizarUsuAsync(Chamado chamado);
        Task<bool> RemoverAsync(Guid id);
        Task<bool> IdExisteAsync(Guid id);
    }
}
