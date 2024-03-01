using backend.DTO;
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

    public async Task createTickets(IEnumerable<Ticket> tickets)
    {
        dbContext.Tickets.AddRange(tickets);
        await dbContext.SaveChangesAsync();
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

    public async Task<RepositoryResponse<Ticket>> getTickets(RepositoryRequest opt)
    {
        var query = dbContext.Tickets.AsQueryable();

        var totalTickets = query.Count();

        IEnumerable<Ticket> tickets;

        if (opt.Page.HasValue && opt.PageLength.HasValue){
            tickets = await query.Skip(opt.Page.Value * opt.PageLength.Value)
                                    .Take(opt.PageLength.Value)
                                    .ToListAsync();
        }else{
            tickets = await query.ToListAsync();
        }
        

        return new RepositoryResponse<Ticket> {
            Items = tickets,
            TotalItems = totalTickets
        };
    }

    public async Task<Ticket?> updateTicket(Ticket newticket)
    {
        var updateRes = dbContext.Tickets.Update(newticket);

        await dbContext.SaveChangesAsync();

        return updateRes.Entity;

    }
}