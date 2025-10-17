using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.EntityFrameworkCore;
using IA_RoboBerto.Contratos.ContratosRepositorio;

namespace IA_RoboBerto.Repositorio
{
    public class MensagensRepositorio : IMensagensRepositorio
    {
        private readonly RoboBertoContext _context;
        public MensagensRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public async Task <PagedList<Mensagem>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _context.Mensagem
                .Skip(tamanho * paginaAtual).Include(m => m.Autor)
                .Take(tamanho)
                .ToListAsync();
            var totalRegistros = await _context.Departamento.CountAsync();
            var resultadoPaginado = new PagedList<Mensagem>(resultado, paginaAtual, tamanho, totalRegistros);

            return resultadoPaginado;
        }
    }
}
