using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace IA_RoboBerto.Repositorio
{
    public class MensagensRepositorio
    {
        private readonly RoboBertoContext _context;
        public MensagensRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public PagedList<Mensagem> ListarTodos(int paginaAtual, int tamanho)
        {
            var resultado = _context.Mensagem
                .Skip(tamanho * paginaAtual).Include(m => m.Autor)
                .Take(tamanho)
                .ToList();
            var totalRegistros = _context.Departamento.Count();
            var resultadoPaginado = new PagedList<Mensagem>(resultado, paginaAtual, tamanho, totalRegistros);

            return resultadoPaginado;
        }
    }
}
