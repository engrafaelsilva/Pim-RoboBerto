
using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class DepartamentoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IList<UsuarioMinDTO> Usuarios { get; } = new List<UsuarioMinDTO>();
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
            foreach(Usuario u in entidade.Usuarios) 
            {
                Usuarios.Add(new UsuarioMinDTO(u));
            }
        }
    }
}
