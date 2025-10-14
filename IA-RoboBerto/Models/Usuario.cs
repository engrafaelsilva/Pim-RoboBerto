using System.ComponentModel.DataAnnotations;

namespace IA_RoboBerto.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCriacao { get; set; }
        public Departamento Departamento { get; set; }
        public Role Role { get; set; }
        public Usuario()
        {
        }

        public Usuario(string nome, string email, string senhaHash, string telefone, DateTime dataCriacao, Departamento departamento, Role role)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Telefone = telefone;
            DataCriacao = dataCriacao;
            Departamento = departamento;
            Role = role;
        }


        //  public int Id { get; set; }
        //
        //  [Required(ErrorMessage = "O nome é obrigatório.")]
        //  [StringLength(100)]
        //  public string Nome { get; set; }
        //
        //  [Required(ErrorMessage = "O e-mail é obrigatório.")]
        //  [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        //  public string Email { get; set; }
        //
        //  [Required(ErrorMessage = "A senha é obrigatória.")]
        //  [DataType(DataType.Password)]
        //  public string Senha { get; set; }
        //
        //  // Indica se o usuário é um cliente (padrão) ou um técnico/administrador
        //  public bool EhTecnico { get; set; } = false;
        //
        //  // Relação 1-para-M (um usuário pode ter muitos chamados
        //  public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}