namespace backend.DTO.Ticket
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Prioridade { get; set; }
        public int ProdutoId { get; set; }
    }
}