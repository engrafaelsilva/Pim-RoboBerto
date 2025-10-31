using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class CategoriaMinDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }



        public CategoriaMinDTO()
        {
        }
        public CategoriaMinDTO(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public CategoriaMinDTO(Categoria entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
        }
    }
}
