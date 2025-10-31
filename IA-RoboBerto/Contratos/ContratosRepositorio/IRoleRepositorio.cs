using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IRoleRepositorio
    {
        Task<IEnumerable<Role>> ListarTodosAsync();
        Task<Role?> ObterPorIdAsync(Guid id);
        Task<Role?> ObterPorNomeAsync(string nome);
    }
}
