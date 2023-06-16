using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Ticket.Request;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Ticket
{
    public record UpdateTicketCommand (TicketCreateRequest request, int id) : IRequest<Result<bool>>
    {
    }

    public class UpdateTicketHandler : IRequestHandler<UpdateTicketCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateTicketHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await unitOfWork.ticketRepository.GetByIdAsync(request.id);
            if (ticket == null)
                return new Result<bool>
                {
                    Errors = new List<string> { "this ticket does not exist" },
                    IsSuccess = false,
                };
            mapper.Map<TicketCreateRequest, BusLine.Data.Models.Ticket>(request.request, ticket);
            var res = await unitOfWork.CompleteAsync();
            if(res)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };
            
        }
    }
}
