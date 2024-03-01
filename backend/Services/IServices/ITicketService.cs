using backend.DTO;
using backend.DTO.Ticket;

namespace backend.Services.IServices
{
    public interface ITicketService
    {
        Task<TicketDTO> CreateTicket(CreateTicketDTO newTicket);
        Task<RepositoryResponse<TicketDTO>> GetTickets(QueryOptionsDTO queryOptionsDTO);
        Task<TicketDTO?> GetTicketById(int id);
        Task<TicketDTO?> UpdateTicket(int id, UpdateTicketDTO newticket);
    }
}   