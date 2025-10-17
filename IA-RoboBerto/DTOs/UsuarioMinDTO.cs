using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class UsuarioMinDTO
    {
        public Guid Id { get; set; }

       public string Name { get; set; }
        public UsuarioMinDTO()
        {
        }
        public UsuarioMinDTO(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public UsuarioMinDTO(Usuario entidade)
        {
            if (entidade == null) return;
            Id = entidade.Id;
            Name = entidade.Nome;
        }
    }
}
