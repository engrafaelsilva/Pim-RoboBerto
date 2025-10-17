using IA_RoboBerto.Modelos;
using IA_RoboBerto.Models.Enums;

namespace IA_RoboBerto.DTOs
{
    public class ChamadoDTO
    {
        public Guid Id { get; set; }
        public UsuarioMinDTO Autor { get; set; }
        public UsuarioMinDTO? Tecnico { get; set; }
        public CategoriaNomeDTO Categoria { get; set; }
        public EStatusChamado Status { get; set; }
        public EPrioridade Prioridade { get; set; }
        public string Titulo { get; set; }
        public string SugestaoGemini { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public DateTime SlaVenceEm { get; set; }
        public bool? SugestaoResolveu { get; set; }
        public IList<MensagemDTO>? Mensagens { get; } = new List<MensagemDTO>();

        public ChamadoDTO()
        {
        }

        public ChamadoDTO(UsuarioMinDTO autor, UsuarioMinDTO tecnico, CategoriaNomeDTO categoria, EStatusChamado status, EPrioridade prioridade, string titulo, string sugestaoGemini, string descricao, DateTime dataAbertura, DateTime dataFechamento, DateTime slaVenceEm, bool sugestaoResolveu)
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

        public ChamadoDTO(Chamado entidade)
        {
            Id = entidade.Id;
            Autor = new UsuarioMinDTO(entidade.Autor);
            Tecnico = entidade.Tecnico != null ? new UsuarioMinDTO(entidade.Tecnico) : null;
            Categoria = new CategoriaNomeDTO(entidade.Categoria);
            Status = entidade.Status;
            Prioridade = entidade.Prioridade;
            Titulo = entidade.Titulo;
            SugestaoGemini = entidade.SugestaoGemini;
            Descricao = entidade.Descricao;
            DataAbertura = entidade.DataAbertura;
            DataFechamento = entidade.DataFechamento != null ? entidade.DataFechamento : null;
            SlaVenceEm = entidade.SlaVenceEm;
            SugestaoResolveu = entidade.SugestaoResolveu != null ? entidade.SugestaoResolveu : null;
            if(entidade.Mensagens.Any())
            {
                foreach (Mensagem MenDto in entidade.Mensagens)
                {
                    Mensagens.Add(new MensagemDTO(MenDto));
                }
            }
            else
            {
                Mensagens = null;
            }
         

          
        }
    }
}
