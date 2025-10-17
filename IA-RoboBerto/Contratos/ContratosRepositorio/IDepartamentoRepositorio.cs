using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IDepartamentoRepositorio
    {
        PagedList<Departamento> ListarTodosDepComUsuarios(int pagina, int tamanho);
    }
}
