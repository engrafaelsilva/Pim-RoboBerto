using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class CategoriaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public IList<Chamado> Chamados { get; set; } = new List<Chamado>();


        public CategoriaDTO()
        {
        }
        public CategoriaDTO(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public CategoriaDTO(Categoria entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
        }
    }
}
