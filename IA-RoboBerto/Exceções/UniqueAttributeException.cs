namespace IA_RoboBerto.Exceções
{
    public class UniqueAttributeException : Exception
    {
        public UniqueAttributeException() : base() { }

        public UniqueAttributeException(string message) : base(message) { }
    }
}
