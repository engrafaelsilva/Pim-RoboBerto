using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Repositorio;

namespace IA_RoboBerto.Servico
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _repo;
        public UsuarioServico(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }

      public PagedList<UsuarioMaxDTO> ListarTodos(int paginaAtual, int tamanho)
        {
            var resultado = _repo.ListarTodos(paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new UsuarioMaxDTO(x)).ToList();
            return new PagedList<UsuarioMaxDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }
    }
}
