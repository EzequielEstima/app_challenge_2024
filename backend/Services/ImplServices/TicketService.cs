using backend.DTO.Ticket;
using backend.Mapper;
using backend.Models;
using backend.Repos.IRepos;
using backend.Services.IServices;
using Microsoft.VisualBasic;

namespace backend.Services.ImplServices;

public class TicketService : ITicketService
{
    private ITicketRepo ticketRepo;
    private IProductRepo productRepo;

    static List<Ticket> tickets = new List<Ticket> 
    {
        new Ticket { TicketId = 1, Titulo = "Habibi plz help", Descricao = "Yes habibi", Prioridade = 1, ProdutoId = 3 },
        new Ticket { TicketId = 2, Titulo = "I'm under the water", Descricao = "GLU GLU", Prioridade = 3, ProdutoId = 2},
        new Ticket { TicketId = 3, Titulo = "Ticket 3", Descricao = "Descrição do Ticket 3", Prioridade = 5, ProdutoId = 1},
    };

    public TicketService(ITicketRepo ticketRepo, IProductRepo productRepo)
    {
        this.ticketRepo = ticketRepo;
        this.productRepo = productRepo;
    }

    public async Task<TicketDTO> CreateTicket(CreateTicketDTO newTicket)
    {

        var product = await productRepo.getProductById(newTicket.ProdutoId);

        if(product == null) {
            throw new Exception("Produto não encontrado");
        }

        var ticket = await ticketRepo.createTicket(new Ticket {
            Titulo = newTicket.Titulo,
            Descricao = newTicket.Descricao,
            Prioridade = newTicket.Prioridade,
            ProdutoId = newTicket.ProdutoId,
        });

        return TicketMapper.ToTicketDTO(ticket);
    }


    public async Task<ListTicketDTO> GetTickets()
    {
        return TicketMapper.ToListTicketDTO(await ticketRepo.getTickets());
    }
    public async Task<TicketDTO?> GetTicketById(int id)
    {
        var ticket = await ticketRepo.getTicketById(id);

        if(ticket == null) {
            return null;
        }

        return TicketMapper.ToTicketDTO(ticket);

    }

    public async Task<TicketDTO?> UpdateTicket(int id, UpdateTicketDTO newticket)
    {
        //FInd
        var ticket = await ticketRepo.getTicketById(id);

        if(ticket == null) {
            return null;
        }

        //Update
        ticket.Titulo = newticket.Titulo;
        ticket.Descricao = newticket.Descricao;
        ticket.Prioridade = newticket.Prioridade;
        ticket.ProdutoId = newticket.ProdutoId;

        //Save
        var updatedTicket = await ticketRepo.updateTicket(ticket);

        if(updatedTicket == null) {
            return null;
        }
    
        return TicketMapper.ToTicketDTO(updatedTicket);
    }
}