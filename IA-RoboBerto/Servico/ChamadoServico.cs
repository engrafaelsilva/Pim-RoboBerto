using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;
using System.Linq;

namespace IA_RoboBerto.Servico
{
    public class ChamadoServico : IChamadoServico
    {
        private readonly IChamadoRepositorio _repo;
        public ChamadoServico(IChamadoRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<PagedList<ChamadoDTO>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _repo.ListarTodosAsync(paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new ChamadoDTO(x)).ToList();

            return new PagedList<ChamadoDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }
    }
}
