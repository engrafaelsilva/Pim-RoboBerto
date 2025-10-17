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
         var resultado = await _context.Usuario.Include(u => u.Roles)
                                         .Include(u => u.Departamento)
             .Skip(tamanho * paginaAtual)
             .Take(tamanho)
             .ToListAsync();
         var totalRegistros = await _context.Usuario.CountAsync();
         var resultadoPaginado = new PagedList<Usuario>(resultado, paginaAtual, tamanho, totalRegistros);
         return resultadoPaginado;
     }
   }
}
