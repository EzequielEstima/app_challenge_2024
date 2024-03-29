using backend.DTO.Ticket;
using backend.Models;


namespace backend.Mapper
{
    public static class TicketMapper
    {

        public static TicketDTO ToTicketDTO(Ticket ticket)
        {
            return new TicketDTO
            {
                TicketId = ticket.TicketId,
                Titulo = ticket.Titulo,
                Descricao = ticket.Descricao,
                Prioridade = ticket.Prioridade,
                ProdutoId = ticket.ProdutoId
            };
        }
    }
}