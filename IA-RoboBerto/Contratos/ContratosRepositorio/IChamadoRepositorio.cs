using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Models.Enums;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IChamadoRepositorio
    {
        Task<PagedList<Chamado>> ListarTodosAsync(int pagina, int tamanho);
        Task<Chamado?> ObterPorIdAsync(Guid id);
        Task<Chamado> AdicionarAsync(Chamado chamado);
        Task<Chamado?> AtualizarUsuAsync(Chamado chamado);
        Task<bool> RemoverAsync(Guid id);
        Task<PagedList<Chamado>> ListarMeusChamadosAsync(Guid id, int paginaAtual, int tamanho);
        Task<Chamado?> AlterarResolveuGeminiEStatusAsync(Chamado chamado, bool resolveuSugestao);
        Task<Chamado?> AlterarStatusParaCanceladoAsync(Guid id);
        Task<Chamado?> AtribuirTecnicoAsync(Chamado chamado, Usuario tecnico);
        Task<Chamado?> ReabrirChamadoAsync(Chamado chamado);
        Task<List<Chamado>> ListarChamadosAbertoExpiradosAsync();
        Task<List<Chamado>> ListarChamadosAbertoNaoExpiradosAsync();
        Task<bool> IdExisteAsync(Guid id);
        Task<Chamado?> AlterarStatusParaFechadoAsync(Guid id);
        Task<Chamado?> AdicionarComentarioAsync(Chamado chamado, Mensagem mensagem);
        Task<List<Chamado>> ListarChamadosAbertoNaoExpiradosAcatadosAsync(Guid tecnicoId);
    }
}
