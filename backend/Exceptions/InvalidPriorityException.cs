namespace backend.Exceptions
{
    public class InvalidPriorityException : Exception
    {
        public InvalidPriorityException() { }
        public InvalidPriorityException(string message) : base(message) { }
    }
}