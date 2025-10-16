using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IDepartamentoRepositorio
    {
        IEnumerable<Departamento> ListarTodos();
    }
}
