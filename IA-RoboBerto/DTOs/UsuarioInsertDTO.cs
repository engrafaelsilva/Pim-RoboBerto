using IA_RoboBerto.Modelos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IA_RoboBerto.DTOs
{
    public class UsuarioInsertDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Erro: O nome é obrigatório.")]
        [StringLength(255)]
        public string Nome { get; set; }

  
        [Required(ErrorMessage = "Erro: O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Erro: Insira um e-mail valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Erro: O telefone é obrigatório.")]
        [StringLength(255)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Erro: A senha é obrigatório.")]
        [StringLength(255)]
        public string SenhaHash { get; set; }
     //   public DateTime DataCriacao { get; set; }

       // [Required(ErrorMessage = "O departamento é obrigatório.")]
        // public DepartamentoNomeDTO Departamento { get; set; }
      //  public IList<RoleDTO> Roles { get; set; } = new List<RoleDTO>();

        public UsuarioInsertDTO()
        {
        }

        public UsuarioInsertDTO(string nome, string email, string senhaHash, string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
           // Departamento = departamento;
            SenhaHash = senhaHash;
        }

        public UsuarioInsertDTO(Usuario entidade)
        {

            Nome = entidade.Nome;
            Email = entidade.Email;
            SenhaHash = entidade.SenhaHash;
            Telefone = entidade.Telefone;
         //   Departamento = new DepartamentoNomeDTO(entidade.Departamento);
         // foreach (Role role in entidade.Roles)
         // {
         //     Roles.Add(new RoleDTO(role));
         // }
        }
    }
}
