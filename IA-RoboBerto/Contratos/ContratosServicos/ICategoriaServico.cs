using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface ICategoriaServico
    {
        PagedList<CategoriaDTO> ListarTodos(int paginaAtual, int tamanho);
    }
}
