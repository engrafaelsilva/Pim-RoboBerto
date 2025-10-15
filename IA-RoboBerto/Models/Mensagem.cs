using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.Models
{
    public class Mensagem
    {
  
        public Guid Id { get; set; }
        public Usuario Autor { get; set; }
        public  DateTime DataHoraMensagem { get; set; }
        public string Texto { get; set; }

        public Mensagem()
        {
        }

        public Mensagem(Usuario autor, DateTime dataHoraMensagem, string texto)
        {
            Id = Guid.NewGuid();
            Autor = autor;
            DataHoraMensagem = dataHoraMensagem;
            Texto = texto;
        }
    }
}
