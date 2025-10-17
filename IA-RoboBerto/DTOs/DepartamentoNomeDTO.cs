using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class DepartamentoNomeDTO
    {
        public string Nome { get; set; }
        public DepartamentoNomeDTO()
        {
        }
        public DepartamentoNomeDTO(string nome)
        {
            Nome = nome;
        }
        public DepartamentoNomeDTO(Departamento entidade)
        {
            Nome = entidade.Nome;
        }
    }
}
