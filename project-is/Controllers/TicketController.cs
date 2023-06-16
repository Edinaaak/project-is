using BusLine.Contracts.Models.Ticket.Request;
using BusLine.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.Ticket;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITicketRepository ticketRepository;
        public TicketController(IMediator mediator, ITicketRepository ticketRepository)
        {
            this.mediator = mediator;
            this.ticketRepository = ticketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await mediator.Send(new GetTicketsQuery());
            return Ok(lista.Data);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            var result = await mediator.Send(new GetTicketQuery(id));
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Errors.FirstOrDefault());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketCreateRequest request)
        {
            var result = await mediator.Send(new CreateTicketCommand(request));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteTicketCommand(id));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TicketCreateRequest request)
        {
            var result = await mediator.Send(new UpdateTicketCommand(request, id));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("add-ticket")]
        public async Task<IActionResult> reserved([FromBody] TicketReserveRequest request)
        {
            var result = await ticketRepository.ReserveTicket(request.numTicket, request.travelId);
            if(!string.IsNullOrEmpty(result.Error))
                return BadRequest(result.Error);
            return Ok(result);
        }

        [HttpGet("seats/{id}")]
        public async Task<IActionResult> getSeats(int id)
        {
            var num = await ticketRepository.GetFreeSeat(id);
            return Ok(num); 
        }
    }
}
