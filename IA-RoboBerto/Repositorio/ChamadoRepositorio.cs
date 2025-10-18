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

        public async Task<Chamado?> ObterPorIdAsync(Guid id)
        {
            return await _context.Chamado
                .Include(c => c.Autor)
                .Include(c => c.Tecnico)
                .Include(c => c.Categoria)
                .Include(c => c.Mensagens)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Chamado> AdicionarAsync(Chamado chamado)
        {
            await _context.Chamado.AddAsync(chamado);
            await _context.SaveChangesAsync();

            return chamado;
        }

        public async Task<Chamado?> AtualizarUsuAsync(Chamado chamado)
        {
            var existente = await _context.Chamado.FindAsync(chamado.Id);
            if (existente == null) return null;

            existente.Categoria = chamado.Categoria;
            existente.Status = chamado.Status;
            existente.Prioridade = chamado.Prioridade;

            _context.Chamado.Update(existente);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var existente = await _context.Chamado.FindAsync(id);
            if (existente == null) return false;

            _context.Chamado.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IdExisteAsync(Guid id)
        {
            return await _context.Chamado.AnyAsync(u => u.Id == id);
        }
    }
}
