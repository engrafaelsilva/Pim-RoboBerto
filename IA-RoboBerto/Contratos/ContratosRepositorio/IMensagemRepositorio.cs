using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IMensagemRepositorio
    {
        PagedList<Mensagem> ListarTodos(int pagina, int tamanho);
    }
}
 