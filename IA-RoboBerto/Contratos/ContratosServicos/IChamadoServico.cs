using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IChamadoServico
    {
        Task<PagedList<ChamadoDTO>> ListarTodosAsync(int paginaAtual, int tamanho);
        Task<ChamadoDTO?> ObterPorIdAsync(Guid id);
        Task<ChamadoDTO?> AtualizarUsuAsync(Guid id, ChamadoDTO dto);
        Task<ChamadoDTO> AbrirChamadoAsync(ChamadoInsertDTO dto);
        Task<PagedList<ChamadoDTO>> ListarMeusChamadosAsync(int paginaAtual, int tamanho);
        Task<ChamadoDTO?> CancelarChamadoAsync(Guid id);
        Task<ChamadoDTO?> ReabrirChamadoAsync(Guid id);
        Task<bool> RemoverAsync(Guid id);
        Task<List<ChamadoDTO>> ListarChamadosAbertoExpiradosAsync();
        Task<List<ChamadoDTO>> ListarChamadosAbertoNaoExpiradosAsync();
        Task<ChamadoDTO> AtribuirTecnicoAsync(Guid chamadoId);
        Task<ChamadoDTO> AlterarSugestaoResolveuEStatusAsync(Guid chamadoId, bool sugestaoResolveu);
        Task<ChamadoDTO?> ComentarNoChamadoAsync(Guid id, MensagemInsertDTO dtoMensagem);
        Task<ChamadoDTO?> FecharChamadoAsync(Guid id);
        Task<List<ChamadoDTO>> ListarChamadosAbertoNaoExpiradosAcatadosAsync();
    }
}
