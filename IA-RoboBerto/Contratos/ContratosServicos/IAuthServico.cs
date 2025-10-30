using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IAuthServico
    {
        Task<Usuario?> ObterUsuarioLogadoAsync();
        Task ValidateSelfOrAdminAsync(Guid id);

    }
}
