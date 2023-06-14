using BusLine.Contracts.Models;
using BusLine.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace project_is.Mediator.Malfunction
{
    public record GetMalfunctionsQuery : IRequest<Result<List<BusLine.Data.Models.Malfunction>>>
    {
    }

    public class GetMalfunctionHadnler : IRequestHandler<GetMalfunctionsQuery, Result<List<BusLine.Data.Models.Malfunction>>>
    {
        private readonly DataContext context;
        public GetMalfunctionHadnler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Result<List<BusLine.Data.Models.Malfunction>>> Handle(GetMalfunctionsQuery request, CancellationToken cancellationToken)
        {
            var lista = await context.malfunctions.Include(x => x.Bus).ToListAsync();
            if (lista == null)
                return new Result<List<BusLine.Data.Models.Malfunction>>
                { 
                    Errors = new List<string> { "list is empty"},
                    IsSuccess = false
                };
            return new Result<List<BusLine.Data.Models.Malfunction>>
            {    Data = lista,
                IsSuccess = true
            };
            
        }
    }
}
