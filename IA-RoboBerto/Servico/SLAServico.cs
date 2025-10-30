using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Servico
{
    public class SLAServico : ISLAServico
    {
        public DateTime CalcularSLA(Chamado chamado)
        {
            DateTime VencimentoSla = DateTime.MinValue;
            if(chamado.Prioridade == Models.Enums.EPrioridade.NORMAL)
            {
                VencimentoSla = chamado.DataAbertura.AddDays(2);
            }
            else if(chamado.Prioridade == Models.Enums.EPrioridade.BAIXA)
            {
                VencimentoSla = chamado.DataAbertura.AddDays(3);
            }
            else if (chamado.Prioridade == Models.Enums.EPrioridade.URGENTE)
            {
                VencimentoSla = chamado.DataAbertura.AddDays(1);
            }
            return VencimentoSla;
        }
    }
}
