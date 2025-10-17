using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IChamadoRepositorio
    {
        Task<PagedList<Chamado>> ListarTodosAsync(int pagina, int tamanho);
    }
}
