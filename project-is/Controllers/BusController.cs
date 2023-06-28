using BusLine.Contracts.Models.Bus.BusRequest;
using BusLine.Contracts.Models.Bus.Request;
using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using project_is.Mediator.Bus;

namespace project_is.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IMediator mediator;
        public BusController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await mediator.Send(new GetBusesQuery());
            return Ok(lista.Data);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetBusQuery(id));
            if(!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] BusCreateRequest request)
        {
            var result = await mediator.Send(new CreateBusCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]BusUpdateRequest request)
        {
            var res = await mediator.Send(new UpdateBusCommand(request, id));
            if(!res.IsSuccess)
                return BadRequest(res.Errors.FirstOrDefault());
            return Ok(res.IsSuccess);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteBusCommand(id));
            if(!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpGet("busDriver/{id}")]
        public async Task<IActionResult> busesDriver(string id)
        {
            var list = await mediator.Send(new getBusesForDriverQuery(id));
            if(list.IsSuccess) 
                return Ok(list.Data);
            return BadRequest(list.Errors.FirstOrDefault());
        }

        [HttpPost("report-failure")]
        public async Task<IActionResult> report([FromBody] MalfunctionCreateRequest request)
        {
            var result = await mediator.Send(new ReportFailureCommand(request));
            if(result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.IsSuccess);
        }

    }
}
