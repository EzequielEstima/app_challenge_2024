using backend.DTO;
using backend.DTO.Ticket;
using backend.Exceptions;
using backend.Execptions;
using backend.Models;
using backend.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<RepositoryResponse<TicketDTO>>> GetTickets([FromQuery] QueryOptionsDTO queryOptionsDTO)
    {    
        return await ticketService.GetTickets(queryOptionsDTO);
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
        TicketDTO? ticket;     
        try
        {
            ticket = await ticketService.CreateTicket(createTicketDTO);
        }
        catch(ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(InvalidPriorityException e)
        {
            return BadRequest(e.Message);
        }

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