
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class DepartamentoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IList<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public DepartamentoDTO()
        {
        }
        public DepartamentoDTO(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }
        public DepartamentoDTO(Departamento entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
        }
    }
}
