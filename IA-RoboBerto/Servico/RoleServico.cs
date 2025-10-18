using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Exceções;
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

        public async Task<IEnumerable<RoleDTO>> ListarTodosAsync()
        {
            var resultado = await _repo.ListarTodosAsync();
            return resultado.Select(x => new RoleDTO(x));
        }
        public async Task<RoleDTO?> ObterPorIdAsync(Guid id)
        {
            Role role = await _repo.ObterPorIdAsync(id);

            if (role == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }

            return new RoleDTO(role);
        }

        public async Task<RoleDTO?> ObterPorNomeAsync(string nome)
        {
            var role = await _repo.ObterPorNomeAsync(nome);
            if (role == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }
            return new RoleDTO(role);
        }
    }
}
