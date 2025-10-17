using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IDepartamentoRepositorio
    {
        Task<PagedList<Departamento>> ListarTodosDepComUsuariosAsync(int pagina, int tamanho);
    }
}
