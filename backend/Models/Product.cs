namespace backend.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Nome { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}