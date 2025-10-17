using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.EntityFrameworkCore;
using IA_RoboBerto.Contratos.ContratosRepositorio;

namespace IA_RoboBerto.Repositorio
{
    public class ChamadoRepositorio : IChamadoRepositorio
    {

        private readonly RoboBertoContext _context;

        public ChamadoRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Chamado>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _context.Chamado
                .Include(c => c.Autor)
                .Include(c => c.Tecnico)
                .Include(c => c.Categoria)
                .Include(c => c.Mensagens)
                .Skip(tamanho * paginaAtual)
                .Take(tamanho)
                .ToListAsync();
            var totalRegistros = await _context.Departamento.CountAsync();
            var resultadoPaginado = new PagedList<Chamado>(resultado, paginaAtual, tamanho, totalRegistros);
            return resultadoPaginado;
        }
    }
}
