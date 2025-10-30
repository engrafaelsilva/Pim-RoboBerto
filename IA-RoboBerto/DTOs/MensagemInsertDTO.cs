using System.ComponentModel.DataAnnotations;

namespace IA_RoboBerto.DTOs
{
    public class MensagemInsertDTO
    {
        // public Guid Id { get; set; }

        [Required(ErrorMessage = "Erro: Campo é requerido")]
        public string Texto { get; set; }

        public MensagemInsertDTO()
        {
        }

        public MensagemInsertDTO(string texto)
        {
            Texto = texto;
        }
    }
}
