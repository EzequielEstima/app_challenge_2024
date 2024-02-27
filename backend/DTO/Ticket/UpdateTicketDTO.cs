namespace backend.DTO.Ticket
{
    public class UpdateTicketDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Prioridade { get; set; }
        public int ProdutoId { get; set; }
    }
}