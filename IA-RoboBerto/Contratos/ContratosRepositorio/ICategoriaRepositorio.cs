using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface ICategoriaRepositorio
    {
        Task<PagedList<Categoria>> ListarTodosAsync(int pagina, int tamanho);
        Task<Categoria?> ObterPorNomeAsync(string nome);
        Task<Categoria?> ObterPorIdAsync(Guid id);
        Task<Categoria> AdicionarAsync(Categoria categoria);
        Task<Categoria?> AtualizarAsync(Categoria categoria);
        Task<bool> RemoverAsync(Guid id);
        Task<bool> IdExiste(Guid id);
    }
}
