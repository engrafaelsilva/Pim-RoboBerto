using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IRoleRepositorio
    {
        IEnumerable<Role> ListarTodos();
    }
}
