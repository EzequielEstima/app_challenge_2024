using backend.DTO.Ticket;
using backend.Mapper;
using backend.Models;
using backend.Repos.IRepos;
using backend.Services.IServices;

namespace backend.Services.ImplServices;

public class TicketService : ITicketService
{
    //private IProductRepo productRepo;

    static List<Ticket> tickets = new List<Ticket> 
    {
        new Ticket { TicketId = 1, Titulo = "Habibi plz help", Descricao = "Descrição do Ticket 1", Prioridade = 1, ProdutoId = 3 },
        new Ticket { TicketId = 2, Titulo = "Ticket 2", Descricao = "Descrição do Ticket 2", Prioridade = 3, ProdutoId = 2},
        new Ticket { TicketId = 3, Titulo = "Ticket 3", Descricao = "Descrição do Ticket 3", Prioridade = 5, ProdutoId = 1},
    };

    static int nextTicketId = 4;

    public TicketService(/*IProductRepo productRepo*/)
    {
        //this.productRepo = productRepo;
    }

    public Task<TicketDTO> CreateTicket(CreateTicketDTO newTicket)
    {
        var ticket = new Ticket {
            TicketId = nextTicketId++,
            Titulo = newTicket.Titulo,
            Descricao = newTicket.Descricao,
            Prioridade = newTicket.Prioridade,
            ProdutoId = newTicket.ProdutoId
        };

        tickets.Add(ticket); //TODO call repo

        return Task.FromResult(TicketMapper.ToTicketDTO(ticket));
    }

    public Task<TicketDTO?> GetTicketById(int id)
    {
        var ticket = tickets.FirstOrDefault(p => p.TicketId == id);// TODO call repo

        if(ticket == null) {
            return Task.FromResult<TicketDTO?>(null);
        }

        return Task.FromResult<TicketDTO?>(TicketMapper.ToTicketDTO(ticket));

    }

    public Task<ListTicketDTO> GetTickets()
    {
        return Task.FromResult(TicketMapper.ToListTicketDTO(tickets));// TODO call repo
    }

    public Task<TicketDTO?> UpdateTicket(int id, UpdateTicketDTO newticket)
    {
        var ticket = tickets.FirstOrDefault(p => p.TicketId == id);// TODO call repo

        if(ticket == null) {
            return Task.FromResult<TicketDTO?>(null);
        }

        ticket.Titulo = newticket.Titulo;
        ticket.Descricao = newticket.Descricao;
        ticket.Prioridade = newticket.Prioridade;
        ticket.ProdutoId = newticket.ProdutoId;

        return Task.FromResult<TicketDTO?>(TicketMapper.ToTicketDTO(ticket));
    }
}