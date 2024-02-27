using backend.Models;
using backend.Repos.IRepos;
using Microsoft.EntityFrameworkCore;

namespace backend.Repos.ImplRepos;
public class TicketRepo : ITicketRepo
{
    private DomainDBContext dbContext;

    public TicketRepo(DomainDBContext context)
    {
        this.dbContext = context;
    }

    public async Task<Ticket> createTicket(Ticket newTicket)
    {
        dbContext.Tickets.Add(newTicket);
        await dbContext.SaveChangesAsync();

        return newTicket;
    }

    public async Task deleteTicket(int id)
    {
        var ticket = await dbContext.Tickets.FindAsync(id);

        if (ticket == null) return;
        
        dbContext.Tickets.Remove(ticket);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Ticket?> getTicketById(int id)
    {
        return await dbContext.Tickets.FindAsync(id);
    }

    public async Task<IEnumerable<Ticket>> getTickets()
    {
        return await dbContext.Tickets.ToListAsync();
    }

    public async Task<Ticket?> updateTicket(Ticket newticket)
    {
        var updateRes = dbContext.Tickets.Update(newticket);

        await dbContext.SaveChangesAsync();

        return updateRes.Entity;

    }
}