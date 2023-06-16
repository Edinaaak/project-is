using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Ticket.Request;
using BusLine.Data;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Ticket
{
    public record CreateTicketCommand (TicketCreateRequest request) : IRequest<Result<bool>>
    {
    }

    public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateTicketHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var mappedTicket = mapper.Map<BusLine.Data.Models.Ticket>(request.request);
            await unitOfWork.ticketRepository.AddAsync(mappedTicket);
            var result = await unitOfWork.CompleteAsync();
            if(result)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool>
            {
                Errors = new List<string> { "error in saving sata" },
                IsSuccess = false
            };
        }
    }
}
