using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class UsuarioMinDTO
    {
        public Guid Id { get; set; }

       public string Nome { get; set; }
        public UsuarioMinDTO()
        {
        }
        public UsuarioMinDTO(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public UsuarioMinDTO(Usuario entidade)
        {
            if (entidade == null) return;
            Id = entidade.Id;
            Nome = entidade.Nome;
        }
    }
}
