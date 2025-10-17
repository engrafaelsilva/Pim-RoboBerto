using IA_RoboBerto.Modelos;
using IA_RoboBerto.Models.Enums;

namespace IA_RoboBerto.Modelos
{
    public class Chamado
    {
        public Guid Id { get; set; }
        public Usuario Autor { get; set; }
        public Usuario? Tecnico { get; set; }
        public Categoria Categoria { get; set; }
        public EStatusChamado Status { get; set; }
        public EPrioridade Prioridade { get; set; }
        public string Titulo { get; set; }
        public string SugestaoGemini { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public DateTime SlaVenceEm { get; set; }
        public bool? SugestaoResolveu { get; set; }
        public ISet<Mensagem> Mensagens { get;} = new HashSet<Mensagem>();

        public Chamado()
        {
        }

        public Chamado(Usuario autor, Usuario tecnico, Categoria categoria, EStatusChamado status, EPrioridade prioridade, string titulo, string sugestaoGemini, string descricao, DateTime dataAbertura, DateTime dataFechamento, DateTime slaVenceEm, bool sugestaoResolveu)
        {
            Id = Guid.NewGuid();
            Autor = autor;
            Tecnico = tecnico;
            Categoria = categoria;
            Status = status;
            Prioridade = prioridade;
            Titulo = titulo;
            SugestaoGemini = sugestaoGemini;
            Descricao = descricao;
            DataAbertura = dataAbertura;
            DataFechamento = dataFechamento;
            SlaVenceEm = slaVenceEm;
            SugestaoResolveu = sugestaoResolveu;
        }

       
    }
}
