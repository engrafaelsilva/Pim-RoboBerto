using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Servico
{
    public class CategoriaServico : ICategoriaServico
    {
        private readonly ICategoriaRepositorio _repo;
        public CategoriaServico(ICategoriaRepositorio repo)
        {
            _repo = repo;
        }

        public PagedList<CategoriaDTO> ListarTodos(int paginaAtual, int tamanho)
        {
            var resultado = _repo.ListarTodos(paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new CategoriaDTO(x)).ToList();

            return new PagedList<CategoriaDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }
    }
}
