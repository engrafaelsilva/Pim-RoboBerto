using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Models
{
    public class Departamento
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IList<Usuario> Departamentos { get; set; } = new List<Usuario>();
        public Departamento()
        {
        }
        public Departamento(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

    }
}
