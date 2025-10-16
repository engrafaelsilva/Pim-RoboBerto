using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;

namespace IA_RoboBerto.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly RoboBertoContext _context;

        public UsuarioRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> listarTodos()
        {
            return _context.Usuario.ToList();
        }
    }
}
