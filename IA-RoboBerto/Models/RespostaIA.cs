using System;

namespace IA_RoboBerto.Models
{
    public class RespostaIA
    {
        public int Id { get; set; }
        public int ChamadoId { get; set; } // Chave estrangeira para o Chamado
        public Chamado Chamado { get; set; } // Objeto de navegação

        public DateTime DataHora { get; set; }

        // O prompt exato que foi enviado à Gemini
        public string PromptEnviado { get; set; }

        // O texto completo da resposta da Gemini
        public string RespostaRecebida { get; set; }

        // Indica quem gerou a resposta (IA, ou um Técnico que editou a resposta)
        public string Origem { get; set; } = "IA"; // Pode ser "IA", "Tecnico"
    }
}