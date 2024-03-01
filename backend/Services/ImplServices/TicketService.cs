using backend.DTO;
using backend.DTO.Ticket;
using backend.Exceptions;
using backend.Execptions;
using backend.Mapper;
using backend.Models;
using backend.Repos.IRepos;
using backend.Services.IServices;

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
        //TODO prly should move this for now it stays here
        if (newTicket.Prioridade < 1 || newTicket.Prioridade > 5) {
            throw new InvalidPriorityException("Priority should be between 1 and 5");
        }

        var ticket = await ticketRepo.createTicket(new Ticket {
            Titulo = newTicket.Titulo,
            Descricao = newTicket.Descricao,
            Prioridade = newTicket.Prioridade,
            ProdutoId = newTicket.ProdutoId,
        });

        return TicketMapper.ToTicketDTO(ticket);
    }


    public async Task<RepositoryResponse<TicketDTO>> GetTickets(QueryOptionsDTO queryOptionsDTO)
    {

        var repoRequest = new RepositoryRequest {
            Page = queryOptionsDTO.Page,
            PageLength = queryOptionsDTO.PageLength
        };

        var repoRes =  await ticketRepo.getTickets(repoRequest);

        return new RepositoryResponse<TicketDTO> {
            Items = repoRes.Items.Select(TicketMapper.ToTicketDTO), // Transforms each item into a TicketDTO
            TotalItems = repoRes.TotalItems
        }; 
        
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