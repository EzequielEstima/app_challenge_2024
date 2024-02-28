namespace backend.Execptions
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException(string message) : base(message)
        {
        }

        public TicketNotFoundException()
        {
        }
    }
}