using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public LoginDTO()
        {
        }

        public LoginDTO(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
        public LoginDTO(Usuario entidade)
        {
            Email = entidade.Email;
            Senha = entidade.SenhaHash;
        }

    }
}
