using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Ticket.Request;
using BusLine.Contracts.Models.Ticket.Response;
using BusLine.Infrastructure;
using BusLine.Infrastructure.Repositories;
using MediatR;

namespace project_is.Mediator.Ticket
{
    public record TicketReserveCommand (TicketReserveRequest request ) :IRequest<Result<TicketReserveResponse>>
    {
    }

    public class TicketReserveHandler : IRequestHandler<TicketReserveCommand, Result<TicketReserveResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        public TicketReserveHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<TicketReserveResponse>> Handle(TicketReserveCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.ticketRepository.ReserveTicket(request.request.numTicket, request.request.travelId);
            if (!string.IsNullOrEmpty(result.Error))
                return new Result<TicketReserveResponse>
                {
                    Errors = new List<string> { result.Error },
                    IsSuccess = false
                };
            return new Result<TicketReserveResponse>
            { IsSuccess = true,
            Data = result
            };
        }
    }
}
