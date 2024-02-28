using backend.DTO;
using backend.DTO.Ticket;
using backend.Execptions;
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
    public async Task<ActionResult<ListTicketDTO>> GetTickets()
    {    
        return await ticketService.GetTickets();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDTO>> GetTicketById(int id)
    {    
        TicketDTO? ticket;
        try
        {
            ticket = await ticketService.GetTicketById(id);
        }
        catch (TicketNotFoundException e)
        {
            return NotFound(e.Message);
        }
    
        return Ok(ticket);
    }
    [HttpPost]
    public async Task<ActionResult<TicketDTO>> CreateTicket(CreateTicketDTO createTicketDTO)
    {            
        var ticket = await ticketService.CreateTicket(createTicketDTO);

        return CreatedAtAction(nameof(GetTicketById), new { id = ticket.TicketId }, ticket);
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TicketDTO>> UpdateTicket(int id, UpdateTicketDTO createTicketDTO)
    {
        TicketDTO? ticket;

        try
        {
            ticket = await ticketService.UpdateTicket(id, createTicketDTO);
        }
        catch(TicketNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok(ticket);
    }
}