using backend.DTO.Ticket;
using backend.Exceptions;
using backend.Execptions;
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

    public TicketService(ITicketRepo ticketRepo, IProductRepo productRepo)
    {
        this.ticketRepo = ticketRepo;
        this.productRepo = productRepo;
    }

    public async Task<TicketDTO> CreateTicket(CreateTicketDTO newTicket)
    {

        var product = await productRepo.getProductById(newTicket.ProdutoId);

        if(product == null) {
            throw new ProductNotFoundException("Product not found");
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
            throw new TicketNotFoundException("Ticket not found");
        }

        return TicketMapper.ToTicketDTO(ticket);

    }

    public async Task<TicketDTO?> UpdateTicket(int id, UpdateTicketDTO newticket)
    {
        //FInd
        var ticket = await ticketRepo.getTicketById(id);

        if(ticket == null) {
            throw new TicketNotFoundException("Ticket not found");
        }

        //Update
        ticket.Titulo = newticket.Titulo;
        ticket.Descricao = newticket.Descricao;
        ticket.Prioridade = newticket.Prioridade;
        ticket.ProdutoId = newticket.ProdutoId;

        //Save
        var updatedTicket = await ticketRepo.updateTicket(ticket);

        if(updatedTicket == null) {
            throw new Exception("Ticket update failed");
        }
    
        return TicketMapper.ToTicketDTO(updatedTicket);
    }
}