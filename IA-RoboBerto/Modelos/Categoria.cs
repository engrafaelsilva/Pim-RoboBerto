

using IA_RoboBerto.Models;

namespace IA_RoboBerto.Modelos
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ISet<Chamado> Chamados { get; set; } = new HashSet<Chamado>();


        public Categoria()
        {
        }

        public Categoria(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

       
    }
}
