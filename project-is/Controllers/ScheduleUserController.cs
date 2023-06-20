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
        private readonly IScheduleUserRepository scheduleUserRepository;
        public ScheduleUserController(IMediator mediator, IScheduleUserRepository scheduleUserRepository)
        {
            this.mediator = mediator;
            this.scheduleUserRepository = scheduleUserRepository;
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
            var result = await scheduleUserRepository.DeleteDriverFromSchedule(request.IdUser, request.IdSchedule);
            if (result)
                return Ok(result);
            return BadRequest(new { msg = "can not delete this driver" });
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
            var result = await scheduleUserRepository.checkAvailability(request);
            return Ok(result);
        }
    }
}
