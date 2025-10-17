using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<PagedList<Usuario>> ListarTodosAsync(int paginaAtual, int tamanho);
    }
}
