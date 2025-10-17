using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IUsuarioRepositorio
    {
        PagedList<Usuario> ListarTodos(int paginaAtual, int tamanho);
    }
}
