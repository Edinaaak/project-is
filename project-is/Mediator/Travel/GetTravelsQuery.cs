using BusLine.Contracts.Models;
using BusLine.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace project_is.Mediator.Travel
{
    public record GetTravelsQuery : IRequest<Result<List<BusLine.Data.Models.Travel>>>
    {
    }

    public class GetTravelsHandler : IRequestHandler<GetTravelsQuery, Result<List<BusLine.Data.Models.Travel>>>
    {
        private readonly DataContext context;
        public GetTravelsHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Result<List<BusLine.Data.Models.Travel>>> Handle(GetTravelsQuery request, CancellationToken cancellationToken)
        {
            var lista = await context.travels.Include(x => x.Schedule).Include(x=>x.Schedule.BusLine).Include(x => x.Bus).ToListAsync();
            return new Result<List<BusLine.Data.Models.Travel>>()
            {
                Data = lista,
                IsSuccess = true
            };
        }
    }
}
