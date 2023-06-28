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
        public TicketController(IMediator mediator)
        {
            this.mediator = mediator;
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
            var result = await mediator.Send(new TicketReserveCommand(request));    
            if(result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpGet("seats/{id}")]
        public async Task<IActionResult> getSeats(int id)
        {
            var result = await mediator.Send(new GetSeatsQuery(id));
            return Ok(result.Data); 
        }
    }
}
