using backend.DTO;
using backend.DTO.Ticket;
using backend.Models;
using backend.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController : ControllerBase {

    private ITicketService ticketService;

    public TicketsController(ITicketService ticketService)
    {
        this.ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<ListTicketDTO>> getTickets()
    {    
        return await ticketService.GetTickets();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDTO>> getTicketById(int id)
    {    
        var ticket = await ticketService.GetTicketById(id);
        
        if (ticket == null) return NotFound();
        
        return ticket;
    }
    [HttpPost]
    public async Task<ActionResult<TicketDTO>> createTicket(CreateTicketDTO createTicketDTO)
    {            
        var ticket = await ticketService.CreateTicket(createTicketDTO);

        return CreatedAtAction(nameof(getTicketById), new { id = ticket.TicketId }, ticket);
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TicketDTO>> updateTicket(int id, UpdateTicketDTO createTicketDTO)
    {
        
        var ticket = await ticketService.UpdateTicket(id, createTicketDTO);

        if (ticket == null) return NotFound();

        return Ok(ticket);
    }
}