using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class UsuarioMaxDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCriacao { get; set; }
        public DepartamentoNomeDTO Departamento { get; set; }
        public IList<RoleDTO> Roles { get;} = new List<RoleDTO>();

        public UsuarioMaxDTO()
        {
        }

        public UsuarioMaxDTO(Guid id, string nome, string email, string telefone, DateTime dataCriacao, DepartamentoNomeDTO departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            DataCriacao = dataCriacao;
            Departamento = departamento;
        }

        public UsuarioMaxDTO(Usuario entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
            Email = entidade.Email;
            Telefone = entidade.Telefone;
            DataCriacao = entidade.DataCriacao;
            Departamento = new DepartamentoNomeDTO(entidade.Departamento);
            foreach(Role role in entidade.Roles)
            {
                Roles.Add(new RoleDTO(role));
            }
        }
    }
}
