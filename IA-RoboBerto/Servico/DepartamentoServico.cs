using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;

namespace IA_RoboBerto.Servico
{
    public class DepartamentoServico : IDepartamentoServico
    {
        private readonly IDepartamentoRepositorio _repo;
        public DepartamentoServico(IDepartamentoRepositorio repo)
        {
            _repo = repo;
        }

        public IEnumerable<DepartamentoDTO> ListarTodos()
        {
            var resultado = _repo.ListarTodos();
            return resultado.Select(x => new DepartamentoDTO(x));
        }
    }
}
