using BusLine.Contracts.Models;
using BusLine.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace project_is.Mediator.Ticket
{
    public record GetTicketsQuery : IRequest<Result<List<BusLine.Data.Models.Ticket>>>
    {
    }

    public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, Result<List<BusLine.Data.Models.Ticket>>>
    {
        private readonly DataContext context;
        public GetTicketsHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Result<List<BusLine.Data.Models.Ticket>>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var list = await context.tickets.Include(x => x.Travel).ToListAsync();
            return new Result<List<BusLine.Data.Models.Ticket>>()
            {
                Data = list,
                IsSuccess = true
            };
        }
    }
}
