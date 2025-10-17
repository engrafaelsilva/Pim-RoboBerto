using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
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

        public async Task<PagedList<DepartamentoDTO>> ListarTodosDepComUsuariosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _repo.ListarTodosDepComUsuariosAsync(paginaAtual,tamanho);

            var resultadoDTO = resultado.Select(x => new DepartamentoDTO(x)).ToList();

            return new PagedList<DepartamentoDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }
    }
}
