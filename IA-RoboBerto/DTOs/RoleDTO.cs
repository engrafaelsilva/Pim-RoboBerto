using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class RoleDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public RoleDTO()
        {
        }
        public RoleDTO(string name)
        {
            Id = Guid.NewGuid();
            Nome = name;
        }

        public RoleDTO(Role entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
        }
    }
}
