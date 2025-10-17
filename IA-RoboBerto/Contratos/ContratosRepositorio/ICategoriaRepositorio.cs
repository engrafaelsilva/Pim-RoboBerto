using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface ICategoriaRepositorio
    {
        PagedList<Categoria> ListarTodos(int pagina, int tamanho);
    }
}
