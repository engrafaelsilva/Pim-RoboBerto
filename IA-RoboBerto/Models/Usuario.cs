using System.ComponentModel.DataAnnotations;

namespace IA_RoboBerto.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        // Indica se o usuário é um cliente (padrão) ou um técnico/administrador
        public bool EhTecnico { get; set; } = false;

        // Relação 1-para-M (um usuário pode ter muitos chamados
        public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}