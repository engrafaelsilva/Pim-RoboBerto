using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class RoleMinDTO
    {
        public string Nome { get; set; }
        public RoleMinDTO()
        {
        }
        public RoleMinDTO(string nome)
        {
            Nome = nome;
        }
        public RoleMinDTO(Role entidade)
        {
            ;
            Nome = entidade.Nome;
        }
    }
}
