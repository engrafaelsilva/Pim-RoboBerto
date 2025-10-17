using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Contratos.ContratosRepositorio
{
    public interface IDepartamentoRepositorio
    {
        Task<PagedList<Departamento>> ListarTodosDepComUsuariosAsync(int pagina, int tamanho);
        Task<Departamento?> ObterPorNomeAsync(string nome);

        Task<bool> IdExiste(Guid id);

        Task<Departamento?> ObterPorIdAsync(Guid id);

        Task<Departamento> AdicionarAsync(Departamento departamento);

        Task<Departamento?> AtualizarAsync(Departamento departamento);

        Task<bool> RemoverAsync(Guid id);
    }
}
