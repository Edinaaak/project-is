using BusLine.Contracts.Models.Schedule.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.Schedule;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IMediator mediator;
        public ScheduleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await mediator.Send(new GetSchedulesQuery());
            if(list.IsSuccess)
                return Ok(list.Data);
            return BadRequest(list.Errors.FirstOrDefault());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var result = await mediator.Send(new GetScheduleQuery(id));
           if(result.IsSuccess)
                return Ok(result.Data);
           return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScheduleCreateRequest request)
        {
            var  result  = await mediator.Send(new CreateScheduleCommand(request));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteScheduleCommand(id));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScheduleUpdateRequest request)
        {
            var result = await mediator.Send(new UpdateScheduleCommand(request, id));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

    }
}
