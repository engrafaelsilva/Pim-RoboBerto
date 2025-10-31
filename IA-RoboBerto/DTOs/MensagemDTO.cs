using IA_RoboBerto.Modelos;

namespace IA_RoboBerto.DTOs
{
    public class MensagemDTO
    {
        public Guid Id { get; set; }
        public UsuarioMinDTO Autor { get; set; }
        public DateTime DataHoraMensagem { get; set; }
        public string Texto { get; set; }

        public MensagemDTO()
        {
        }

        public MensagemDTO(UsuarioMinDTO autor, DateTime dataHoraMensagem, string texto)
        {
            Id = Guid.NewGuid();
            Autor = autor;
            DataHoraMensagem = dataHoraMensagem;
            Texto = texto;
        }
        public MensagemDTO(Mensagem entidade)
        {
            Id = entidade.Id;
            Autor = new UsuarioMinDTO(entidade.Autor);
            DataHoraMensagem = entidade.DataHoraMensagem;
            Texto = entidade.Texto;
        }
    }
}
