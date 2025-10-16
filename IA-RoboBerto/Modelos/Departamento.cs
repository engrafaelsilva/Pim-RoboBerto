namespace IA_RoboBerto.Modelos
{
    public class Departamento
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IList<Usuario> Usuarios { get; set; } = new List<Usuario>();
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
