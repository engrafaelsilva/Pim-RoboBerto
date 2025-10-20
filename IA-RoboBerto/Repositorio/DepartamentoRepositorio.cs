using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Controladores;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace IA_RoboBerto.Repositorio
{
    public class DepartamentoRepositorio : IDepartamentoRepositorio
    {
        private readonly RoboBertoContext _context;

        public DepartamentoRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Departamento>> ListarTodosDepComUsuariosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _context.Departamento.Include(d => d.Usuarios)
                .AsNoTracking()
                .Skip(tamanho*paginaAtual)
                .Take(tamanho)
                .AsNoTracking()
                .ToListAsync();
            var totalRegistros = await _context.Departamento.CountAsync();
            var resultadoPaginado = new PagedList<Departamento>(resultado,paginaAtual,tamanho,totalRegistros);

            return resultadoPaginado;
        }
        public async Task<Departamento?> ObterPorNomeAsync(string nome)
        {
            return await _context.Departamento
                .Include(d => d.Usuarios)
                 .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Nome.ToUpper().Trim() == nome.ToUpper().Trim());
        }

        public async Task<bool> IdExisteAsync(Guid id)
        {
            return await _context.Departamento
                 .AsNoTracking()
                .AnyAsync(d => d.Id == id);
        }

        public async Task<Departamento?> ObterPorIdAsync(Guid id)
        {
            return await _context.Departamento
                .Include(d => d.Usuarios)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Departamento> AdicionarAsync(Departamento departamento)
        {
            await _context.Departamento.AddAsync(departamento);
            await _context.SaveChangesAsync();
            return departamento;
        }

        public async Task<Departamento?> AtualizarAsync(Departamento departamento)
        {
            var existente = await _context.Departamento.FindAsync(departamento.Id);
            if (existente == null) return null;

            // Atualize os campos que fizerem sentido — aqui atualizo Nome como no exemplo de Categoria
            existente.Nome = departamento.Nome;

            // Se houver outros campos (descrição, código, etc.), atualize aqui também:
            // existente.Descricao = departamento.Descricao;

            _context.Departamento.Update(existente);
            await _context.SaveChangesAsync();

            return existente;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var existente = await _context.Departamento.FindAsync(id);
            if (existente == null) return false;

            _context.Departamento.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

