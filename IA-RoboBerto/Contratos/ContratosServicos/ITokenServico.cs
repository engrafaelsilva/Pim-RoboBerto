using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface ITokenServico
    {
        string GerarToken(Usuario usuario);
    }
}
