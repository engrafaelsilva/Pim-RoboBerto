namespace IA_RoboBerto.Modelos
{
    public class Departamento
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ISet<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
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
