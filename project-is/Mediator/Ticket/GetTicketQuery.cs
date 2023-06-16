using BusLine.Contracts.Models;
using BusLine.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace project_is.Mediator.Ticket
{
    public record GetTicketQuery (int id) : IRequest<Result<BusLine.Data.Models.Ticket>>
    {
    }

    public class GetTicketHandler : IRequestHandler<GetTicketQuery, Result<BusLine.Data.Models.Ticket>>
    {
        private readonly DataContext context;
        public GetTicketHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<Result<BusLine.Data.Models.Ticket>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await context.tickets.Where(x => x.Id == request.id).Include(x => x.Travel).FirstOrDefaultAsync();
            if (ticket == null)
            {
                return new Result<BusLine.Data.Models.Ticket>
                {
                    Errors = new List<string> { "this ticket does not exist" },
                    IsSuccess = false
                };

            }
            return new Result<BusLine.Data.Models.Ticket>
            {
                Data = ticket,
                IsSuccess = true
            };

        }
    }
}
