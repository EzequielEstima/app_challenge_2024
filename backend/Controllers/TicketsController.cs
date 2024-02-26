using backend.Dto;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController : ControllerBase {

    static List<Ticket> tickets = new List<Ticket> {
        new Ticket { Id = 1, Titulo = "Ticket 12222", Descricao = "Descrição do Ticket 1", Prioridade = 1, ProdutoId = 3 },
        new Ticket { Id = 2, Titulo = "Ticket 2", Descricao = "Descrição do Ticket 2", Prioridade = 3, ProdutoId = 2},
        new Ticket { Id = 3, Titulo = "Ticket 3", Descricao = "Descrição do Ticket 3", Prioridade = 5, ProdutoId = 1},
    };

    static int ticketCount = 3;

    [HttpGet]
    public async Task<ActionResult<List<Ticket>>> getTickets(){
        
        return tickets;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> getTicketById(int id){
        
        var ticket = tickets.Find(ticket => ticket.Id == id);
        
        if (ticket == null) return NotFound();
        
        return ticket;
    }
    [HttpPost]
    public async Task<ActionResult<Ticket>> createTicket(CreateTicketDto createTicketDTO){
                
        // TODO : implemnt constructor in the Ticket class

        Ticket ticket = new Ticket {
            Id = ++ticketCount, // first increments the variable, then sets id to the incremented value
            Titulo = createTicketDTO.Titulo,
            Descricao = createTicketDTO.Descricao,
            Prioridade = createTicketDTO.Prioridade,
            ProdutoId = createTicketDTO.ProdutoId
        };
        
        tickets.Add(ticket);
        return CreatedAtAction(nameof(getTicketById), new { id = ticket.Id }, ticket);
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Ticket>> updateTicket(int id, CreateTicketDto createTicketDTO){
        
        var ticket = tickets.Find(ticket => ticket.Id == id);
        
        if (ticket == null) return NotFound();

        ticket.Titulo = createTicketDTO.Titulo;
        ticket.Descricao = createTicketDTO.Descricao;
        ticket.Prioridade = createTicketDTO.Prioridade;
        ticket.ProdutoId = createTicketDTO.ProdutoId;

        return Ok(ticket);
    }
}