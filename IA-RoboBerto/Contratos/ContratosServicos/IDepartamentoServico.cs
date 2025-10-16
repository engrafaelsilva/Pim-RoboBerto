using IA_RoboBerto.DTOs;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IDepartamentoServico
    {
        IEnumerable<DepartamentoDTO> ListarTodos();
    }
}
