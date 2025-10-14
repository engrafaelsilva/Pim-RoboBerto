

using IA_RoboBerto.Models;

namespace IA_RoboBerto.Modelos
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public IList<Chamado> Chamados { get; set; } = new List<Chamado>();


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
