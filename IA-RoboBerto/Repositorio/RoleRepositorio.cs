using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;

namespace IA_RoboBerto.Repositorio
{
    public class RoleRepositorio
    {
        private readonly RoboBertoContext _context;

        public RoleRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> listarTodos()
        {
            return _context.Roles.ToList();
        }

    }
}

