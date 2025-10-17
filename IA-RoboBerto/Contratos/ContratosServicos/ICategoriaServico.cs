using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface ICategoriaServico
    {
        Task<PagedList<CategoriaMinDTO>> ListarTodosAsync(int paginaAtual, int tamanho);
        Task<CategoriaMinDTO?> ObterPorNomeAsync(string nome);
        Task<CategoriaMinDTO?> ObterPorIdAsync(Guid id);
        Task<CategoriaMinDTO> AdicionarAsync(CategoriaMinDTO dto);
        Task<CategoriaMinDTO?> AtualizarAsync(Guid id,CategoriaMinDTO dto);
        Task<bool> RemoverAsync(Guid id);
    }
}
