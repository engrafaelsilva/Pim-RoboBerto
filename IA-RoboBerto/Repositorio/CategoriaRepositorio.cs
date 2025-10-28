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


        public async Task<PagedList<Categoria>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _context.Categoria
                .AsNoTracking()
                .Include(c => c.Chamados)
                .Skip(tamanho * paginaAtual)
                .Take(tamanho)
                .ToListAsync();
            var totalRegistros = await _context.Categoria.CountAsync();
            var resultadoPaginado = new PagedList<Categoria>(resultado, paginaAtual, tamanho, totalRegistros);

            return resultadoPaginado;
        }
        public async Task<Categoria?> ObterPorNomeAsync(string nome)
        {
            return await _context.Categoria
                .Include(c => c.Chamados)
                .FirstOrDefaultAsync(c => c.Nome.ToUpper().Trim() == nome.ToUpper().Trim());
        }

        public async Task<bool> IdExisteAsync(Guid id)
        {
            return await _context.Categoria
                .AnyAsync(c => c.Id == id);
        }

        public async Task<Categoria?> ObterPorIdAsync(Guid id)
        {
            return await _context.Categoria
                .Include(c => c.Chamados)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria> AdicionarAsync(Categoria categoria)
      {
          await _context.Categoria.AddAsync(categoria);
          await _context.SaveChangesAsync();
          return categoria;
      }

     public async Task<Categoria?> AtualizarAsync(Categoria categoria)
     {
         var existente = await _context.Categoria.FindAsync(categoria.Id);
         if (existente == null) return null;
  
         existente.Nome = categoria.Nome;
         _context.Categoria.Update(existente);
         await _context.SaveChangesAsync();
         return existente;
     }
  

      public async Task<bool> RemoverAsync(Guid id)
      {
          var existente = await _context.Categoria.FindAsync(id);
            if (existente == null) return false;
          _context.Categoria.Remove(existente);
          await _context.SaveChangesAsync();
          return true;
      }
    }
}
