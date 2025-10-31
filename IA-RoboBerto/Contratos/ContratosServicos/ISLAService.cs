using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface ISLAService
    {
        DateTime CalcularSLA(Chamado chamado);
    }
}
