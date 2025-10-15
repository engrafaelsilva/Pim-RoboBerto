

namespace IA_RoboBerto.Modelos
{
 
    public class Role
    {

        public Guid Id { get; set; }

        public string Name { get; set; }
        public Role()
        {
        }
        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
