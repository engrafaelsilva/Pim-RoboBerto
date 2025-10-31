using IA_RoboBerto.Modelos;
using IA_RoboBerto.Models.Enums;

namespace IA_RoboBerto.DTOs
{
    public class ChamadoInsertDTO
    {
     //   public Guid Id { get; set; }
        public CategoriaNomeDTO Categoria { get; set; }
        public EPrioridade Prioridade { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<MensagemDTO>? Mensagens { get; } = new List<MensagemDTO>();

        public ChamadoInsertDTO(CategoriaNomeDTO categoria, EPrioridade prioridade, string titulo, string descricao)
        {
            Categoria = categoria;
            Prioridade = prioridade;
            Titulo = titulo;
            Descricao = descricao;

        }
    }
}
