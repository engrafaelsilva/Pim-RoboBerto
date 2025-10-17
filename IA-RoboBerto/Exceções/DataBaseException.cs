namespace IA_RoboBerto.Exceções
{
    public class DataBaseException : Exception
    {
        public DataBaseException() : base() { }

        public DataBaseException(string message) : base(message) { }
    }
}
