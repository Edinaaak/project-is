using BusLine.Contracts.Models.Ticket.Request;
using BusLine.Contracts.Models.Travel.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.Ticket;
using project_is.Mediator.Travel;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelController : ControllerBase
    {
        private readonly IMediator mediator;
        public TravelController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await mediator.Send(new GetTravelsQuery());
            return Ok(list.Data);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetTravelQuery(id));
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Errors.FirstOrDefault());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TravelCreateRequest request)
        {
            var result = await mediator.Send(new CreateTravelCommand(request));
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteTravelCommand(id));
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TravelCreateRequest request)
        {
            var result = await mediator.Send(new UpdateTravelCommand(request, id));
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }
    }
}
