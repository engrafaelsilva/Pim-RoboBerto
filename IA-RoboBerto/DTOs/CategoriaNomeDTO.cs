using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class CategoriaNomeDTO
    {
        public string Nome { get; set; }
        public CategoriaNomeDTO()
        {
        }
        public CategoriaNomeDTO(string name)
        {
            Nome = name;
        }

        public CategoriaNomeDTO(Categoria entidade)
        {
            Nome = entidade.Nome;
        }
    }
}
