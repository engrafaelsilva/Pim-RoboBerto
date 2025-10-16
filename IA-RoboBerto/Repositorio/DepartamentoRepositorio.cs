using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Controladores;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;

namespace IA_RoboBerto.Repositorio
{
    public class DepartamentoRepositorio : IDepartamentoRepositorio
    {
        private readonly RoboBertoContext _context;

        public DepartamentoRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public IEnumerable<Departamento> ListarTodos()
        {
            return _context.Departamento.ToList();
        }


    }
}
