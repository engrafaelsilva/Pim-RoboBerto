using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;

namespace IA_RoboBerto.Servico
{
    public class RoleServico : IRoleServico
    {
        private readonly IRoleRepositorio _repo;
        public RoleServico(IRoleRepositorio repo)
        {
            _repo = repo;
        }

        public IEnumerable<RoleDTO> ListarTodos()
        {
            var resultado = _repo.ListarTodos();
            return resultado.Select(x => new RoleDTO(x));
        }

    }
}
