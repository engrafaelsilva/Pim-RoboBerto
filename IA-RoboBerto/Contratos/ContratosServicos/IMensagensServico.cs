using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IMensagensServico
    {
        PagedList<MensagemDTO> ListarTodos(int paginaAtual, int tamanho);
    }
}
