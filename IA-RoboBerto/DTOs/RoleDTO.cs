using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class RoleDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public RoleDTO()
        {
        }
        public RoleDTO(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public RoleDTO(Role entidade)
        {
            Id = entidade.Id;
            Name = entidade.Name;
        }
    }
}
