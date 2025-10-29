namespace IA_RoboBerto.DTOs
{
    public class MensagemInsertDTO
    {
       // public Guid Id { get; set; }
        public string Texto { get; set; }

        public MensagemInsertDTO()
        {
        }

        public MensagemInsertDTO(string texto)
        {
            Texto = texto;
        }
    }
}
