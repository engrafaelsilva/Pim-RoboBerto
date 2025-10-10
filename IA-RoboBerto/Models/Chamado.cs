namespace IA_RoboBerto.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; } // O que o usuário reportou
        public string RespostaIA { get; set; } // Resposta gerada pela Gemini
        public DateTime DataAbertura { get; set; }
        public bool Resolvido { get; set; }
        public int UsuarioId { get; set; } // Chave estrangeira para o usuário que abriu o chamado
    }
}
