namespace IA_RoboBerto.Contratos.ContratosServicos
{
    public interface IGeminiServico
    {
        Task<string> GerarTextoAsync(string nomeUsuario, string descricaoChamado);
    }
}
