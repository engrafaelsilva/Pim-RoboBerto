using IA_RoboBerto.Modelos;
using IA_RoboBerto.Models.Enums;

namespace IA_RoboBerto.DTOs
{
    public class ChamadoInsertDTO
    {
        public Guid Id { get; set; }
        public UsuarioMinDTO Autor { get; set; }
        public CategoriaNomeDTO Categoria { get; set; }
        public EStatusChamado Status { get; set; }
        public EPrioridade Prioridade { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAbertura { get; set; }
        public bool? SugestaoResolveu { get; set; }
        public IList<MensagemDTO>? Mensagens { get; } = new List<MensagemDTO>();

        public ChamadoInsertDTO(Guid id, UsuarioMinDTO autor, CategoriaNomeDTO categoria, EStatusChamado status, EPrioridade prioridade, string titulo, string descricao, DateTime dataAbertura, bool? sugestaoResolveu)
        {
            Id = id;
            Autor = autor;
            Categoria = categoria;
            Status = status;
            Prioridade = prioridade;
            Titulo = titulo;
            Descricao = descricao;
            DataAbertura = dataAbertura;
            SugestaoResolveu = sugestaoResolveu;
        }
    }
}
