using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IUsuarioServico
    {
        PagedList<UsuarioMaxDTO> ListarTodos(int paginaAtual, int tamanho);
    }
}
