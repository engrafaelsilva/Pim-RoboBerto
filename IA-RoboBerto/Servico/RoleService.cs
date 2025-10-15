using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;

namespace IA_RoboBerto.Servico
{
    public class RoleService
    {
        private readonly RoleRepositorio _repo;
        public RoleService(RoleRepositorio repo)
        {
            _repo = repo;
        }

        public IEnumerable<Role> ListarTodosRoles()
        {
            return _repo.listarTodos();
        }

    }
}
