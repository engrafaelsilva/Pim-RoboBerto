using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.EntityFrameworkCore;
using IA_RoboBerto.Contratos.ContratosRepositorio;

namespace IA_RoboBerto.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly RoboBertoContext _context;

        public CategoriaRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public PagedList<Categoria> ListarTodos(int paginaAtual, int tamanho)
        {
            var resultado = _context.Categoria.Include(c => c.Chamados)
                .Skip(tamanho * paginaAtual)
                .Take(tamanho)
                .ToList();
            var totalRegistros = _context.Departamento.Count();
            var resultadoPaginado = new PagedList<Categoria>(resultado, paginaAtual, tamanho, totalRegistros);

            return resultadoPaginado;
        }
    }
}
