using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;

namespace IA_RoboBerto.Servico
{
    public class UsuarioServico
    {
        private readonly UsuarioRepositorio _repo;
        public UsuarioServico(UsuarioRepositorio repo)
        {
            _repo = repo;
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return _repo.listarTodos();
        }
    }
}
