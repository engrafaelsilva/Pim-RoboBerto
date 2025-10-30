using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface ISLAServico
    {
        DateTime CalcularSLA(Chamado chamado);
    }
}
