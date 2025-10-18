using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class UsuarioInsertDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string SenhaHash { get; set; }
        public DateTime DataCriacao { get; set; }
        public DepartamentoNomeDTO Departamento { get; set; }
        public IList<RoleDTO> Roles { get; set; } = new List<RoleDTO>();

        public UsuarioInsertDTO()
        {
        }

        public UsuarioInsertDTO(Guid id, string nome, string email, string senhaHash, string telefone, DateTime dataCriacao, DepartamentoNomeDTO departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            DataCriacao = dataCriacao;
            Departamento = departamento;
            SenhaHash = senhaHash;
        }

        public UsuarioInsertDTO(Usuario entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
            Email = entidade.Email;
            SenhaHash = entidade.SenhaHash;
            Telefone = entidade.Telefone;
            DataCriacao = entidade.DataCriacao;
            Departamento = new DepartamentoNomeDTO(entidade.Departamento);
            foreach (Role role in entidade.Roles)
            {
                Roles.Add(new RoleDTO(role));
            }
        }
    }
}
