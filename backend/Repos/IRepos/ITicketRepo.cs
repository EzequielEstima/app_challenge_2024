
using backend.Models;

namespace backend.Repos.IRepos;
public interface ITicketRepo
{
    Task<IEnumerable<Ticket>> getTickets();
    Task<Ticket?> getTicketById(int id);
    Task<Ticket> createTicket(Ticket newTicket);
    Task createTickets(IEnumerable<Ticket> tickets);
    Task<Ticket?> updateTicket(Ticket newticket);
    Task deleteTicket(int id);
}