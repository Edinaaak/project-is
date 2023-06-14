using BusLine.Contracts.Models;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace project_is.Mediator.Malfunction
{
    public record GetMalfunctionQuery (int id) : IRequest<Result<BusLine.Data.Models.Malfunction>>
    {
    }
    public class GetMalfunctionHadler : IRequestHandler<GetMalfunctionQuery, Result<BusLine.Data.Models.Malfunction>>
    {
        private readonly DataContext context;
        public GetMalfunctionHadler(DataContext context)
        {
            
            this.context = context;
        }

        public async Task<Result<BusLine.Data.Models.Malfunction>> Handle(GetMalfunctionQuery request, CancellationToken cancellationToken)
        {
            var malfnc = await context.malfunctions.Include(x => x.Bus).Where(x => x.Id == request.id).FirstOrDefaultAsync();
            if(malfnc == null)
                return new Result<BusLine.Data.Models.Malfunction>()
                {
                    Errors = new List<string> { "malfunction does not exist"},
                    IsSuccess = false
                };
            return new Result<BusLine.Data.Models.Malfunction>
            {
                Data = malfnc,
                IsSuccess = true

            };
        }
    }
}
