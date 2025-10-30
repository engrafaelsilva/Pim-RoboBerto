using IA_RoboBerto.Modelos;
using IA_RoboBerto.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace IA_RoboBerto.DTOs
{
    public class ChamadoInsertDTO
    {
        //   public Guid Id { get; set; }

        [Required(ErrorMessage = "Erro: a categoria do chamado é obrigatório.")]
        public CategoriaNomeDTO Categoria { get; set; }

        [Required(ErrorMessage = "Erro: a prioridade do chamado é obrigatório.")]
        public EPrioridade Prioridade { get; set; }

        [Required(ErrorMessage = "Erro: o título do chamado é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Erro: a descrição do chamado é obrigatório.")]
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
