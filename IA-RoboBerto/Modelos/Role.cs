

namespace IA_RoboBerto.Modelos
{
 
    public class Role
    {

        public Guid Id { get; set; }

        public string Nome { get; set; }
        public Role()
        {
        }
        public Role(Guid id, string name)
        {
            Id = id;
            Nome = name;
        }
    }
}
