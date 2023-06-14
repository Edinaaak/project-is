using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.Malfunction;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MalfunctionController : ControllerBase
    {
        private readonly IMediator mediator;
        public MalfunctionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new GetMalfunctionsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await mediator.Send(new GetMalfunctionQuery(id));
            if (res.IsSuccess)
            {
                return Ok(res.Data);
            }

            return Ok(res.IsSuccess);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MalfunctionCreateRequest request)
        {
            var res = await mediator.Send(new CreateMalfunctionCommand(request));
            if (res.IsSuccess)
            {
                return Ok(res.IsSuccess);
            }
            return BadRequest(res.IsSuccess);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var res = await mediator.Send(new DeleteMalfunctionCommand(id));
            if(res.IsSuccess)
                return Ok(res.IsSuccess);
            return BadRequest(res.IsSuccess);
        }
    }
}