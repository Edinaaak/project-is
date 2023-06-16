using BusLine.Contracts.Models;
using BusLine.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace project_is.Mediator.Travel
{
    public record GetTravelQuery (int id) : IRequest<Result<BusLine.Data.Models.Travel>>
    {
    }

    public class GetTravelHandler : IRequestHandler<GetTravelQuery, Result<BusLine.Data.Models.Travel>>
    {
        private readonly DataContext context;
        public GetTravelHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Result<BusLine.Data.Models.Travel>> Handle(GetTravelQuery request, CancellationToken cancellationToken)
        {
            var travel = await context.travels.Include(x => x.Schedule).Include(x => x.Schedule.BusLine).Include(x => x.Bus).Where(x => x.Id == request.id).FirstOrDefaultAsync();
            if (travel == null)
            {
                return new Result<BusLine.Data.Models.Travel> 
                {
                    Errors = new List<string> { "this travel does not exist" },
                    IsSuccess = false 
                };
            }
            return new Result<BusLine.Data.Models.Travel>
            {
                Data = travel,
                IsSuccess = true
            };
        }
    }
}
