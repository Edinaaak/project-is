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
        private readonly ILogger<AuthController> logger;
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(RegisterRequest request)
        {
            var result = await mediator.Send(new RegisterUserCommand(request));
            if (!result.IsSuccess)
            {
                logger.LogError("fail registration");
                return BadRequest(result.Errors.FirstOrDefault());
                
            }
            logger.LogInformation("successfully register");
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
