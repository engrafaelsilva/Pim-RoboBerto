using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace IA_RoboBerto.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly RoboBertoContext _context;

        public UsuarioRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

     public async Task<PagedList<Usuario>> ListarTodosAsync(int paginaAtual, int tamanho)
     {
         var resultado = await _context
             .Usuario
             .AsNoTracking()
             .Include(u => u.Roles)
             .Include(u => u.Departamento)
             .Skip(tamanho * paginaAtual)
             .Take(tamanho)
             .ToListAsync();
         var totalRegistros = await _context.Usuario.CountAsync();
         var resultadoPaginado = new PagedList<Usuario>(resultado, paginaAtual, tamanho, totalRegistros);
         return resultadoPaginado;
     }
        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            return await _context.Usuario
                .Include(u => u.Roles)
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<Usuario?> ObterPorNomeAsync(string nome)
        {
            return await _context.Usuario
                .Include(u => u.Roles)
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(u => u.Nome.ToUpper().Trim() == nome.ToUpper().Trim());
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuario
                .Include(u => u.Roles)
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(u => u.Email.ToUpper().Trim() == email.ToUpper().Trim());
        }

        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

             return usuario;
        }

        public async Task<Usuario?> AtualizarAsync(Usuario usuario)
        {
            var existente = await _context.Usuario.FindAsync(usuario.Id);
            if (existente == null) return null;

            existente.Nome = usuario.Nome;
            existente.Email = usuario.Email;
            existente.Telefone = usuario.Telefone;
            existente.Departamento = usuario.Departamento;

            _context.Usuario.Update(existente);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var existente = await _context.Usuario.FindAsync(id);
            if (existente == null) return false;

            _context.Usuario.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IdExisteAsync(Guid id)
        {
            return await _context.Usuario
                .AnyAsync(u => u.Id == id);
        }
    }
}
