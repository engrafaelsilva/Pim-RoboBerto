using IA_RoboBerto.DTOs;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IRoleServico
    {
        IEnumerable<RoleDTO> ListarTodos();
    }
}
