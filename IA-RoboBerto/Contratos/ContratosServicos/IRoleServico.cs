using IA_RoboBerto.DTOs;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IRoleServico
    {
        Task<IEnumerable<RoleDTO>> ListarTodosAsync();
    }
}
