using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IMensagensRepositorio
    {
        Task <PagedList<Mensagem>> ListarTodosAsync(int pagina, int tamanho);
        Task<Mensagem?> ObterPorIdAsync(Guid id);
        Task<Mensagem> AdicionarAsync(Mensagem mensagem);
    }
}
 