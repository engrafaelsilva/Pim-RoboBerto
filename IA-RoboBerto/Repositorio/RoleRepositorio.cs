using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;

namespace IA_RoboBerto.Repositorio
{
    public class RoleRepositorio : IRoleRepositorio
    {
    private readonly RoboBertoContext _context;

    public RoleRepositorio(RoboBertoContext context)
    {
        _context = context;
    }

        public IEnumerable<Role> ListarTodos()
        {
            return _context.Role.ToList();
        }
    }
}

