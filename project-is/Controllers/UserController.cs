using BusLine.Contracts.Models.User.Request;
using BusLine.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.User;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUserRepository userRepository;
        public UserController(IMediator mediator, IUserRepository userRepository)
        {
            this.mediator = mediator;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await mediator.Send(new GetUsersQuery());
            return Ok(lista.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (string id)
        {
            var result = await mediator.Send(new GetUserQuery(id));
            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Errors.FirstOrDefault());

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (string id)
        {
            var result = await mediator.Send(new DeleteUserCommand(id));
            if(result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]UserUpdateRequest request)
        {
            var result = await mediator.Send(new UpdateUserCommand(request, id));
            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpGet("drivers")]
        public async Task<IActionResult> GetDrivers()
        {
            var list = await mediator.Send(new GetDriverQuery());
            return Ok(list.Data);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> forgotPassword([FromQuery]string email)
        {
            var result = await mediator.Send(new ForgotPasswordCommand(email));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> resetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await mediator.Send(new ResetPasswordCommand(request));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errors.FirstOrDefault());
        }
    }
}
