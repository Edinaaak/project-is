using BusLine.Contracts.Models.Busline.Request;
using BusLine.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_is.Mediator.Busline;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuslineController : ControllerBase
    {
        private readonly IMediator mediator;
        public BuslineController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetBuslinesQuery());
            if(!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetBuslineQuery(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BusLine.Data.Models.BusLine request)
        {
            var result = await mediator.Send(new CreateBuslineCommand(request));
            if(!result.IsSuccess) 
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteBuslineCommand(id));
            if(!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]BuslineCreateRequest request)
        {
            var result = await mediator.Send(new UpdateBuslineCommand(request, id));
            if(!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }


    }
}
