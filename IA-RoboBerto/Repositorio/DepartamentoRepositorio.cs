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
                .Skip(tamanho*paginaAtual)
                .Take(tamanho)
                .ToListAsync();
            var totalRegistros = await _context.Departamento.CountAsync();
            var resultadoPaginado = new PagedList<Departamento>(resultado,paginaAtual,tamanho,totalRegistros);

            return resultadoPaginado;
        }


    }
}
