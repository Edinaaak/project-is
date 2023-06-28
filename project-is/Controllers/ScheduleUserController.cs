using BusLine.Contracts.Models.Schedule;
using BusLine.Contracts.Models.Schedule.Request;
using BusLine.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.ScheduleUser;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleUserController : ControllerBase
    {
        private readonly IMediator mediator;
        public ScheduleUserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await mediator.Send(new GetScheduleUsersQuery());
            return Ok(list.Data);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetScheduleUserQuery(id));
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Crate([FromBody] ScheduleUserCreateRequest request)
        {
            var result = await mediator.Send(new CreateScheduleUserCommand(request));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ScheduleUserDeleteRequest request)
        {
            var result = await mediator.Send(new DeleteScheduleUserCommand(request));
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScheduleUserCreateRequest request)
        {
            var result = await mediator.Send(new UpdateScheduleUserCommand(request, id));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("driverSchedule")]
        public async Task<IActionResult> GetDriversSchedule([FromBody] ScheduUserCheckRequest request)
        {
            var result = await mediator.Send(new GetDriverScheduleQuery(request));
            if(result.IsSuccess)
            return Ok(result.IsSuccess);
            return BadRequest(result.IsSuccess);
        }
    }
}
