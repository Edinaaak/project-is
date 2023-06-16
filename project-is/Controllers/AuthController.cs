using BusLine.Contracts.Models.User.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.User;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(RegisterRequest request)
        {
            var result = await mediator.Send(new RegisterUserCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {

            var result = await mediator.Send(new LoginUserCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
    }
}
