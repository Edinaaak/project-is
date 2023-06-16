using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using project_is.Mediator.Travel;

namespace project_is.Mediator.Ticket
{
    public record DeleteTicketCommand (int id) : IRequest<Result<bool>>
    {
    }

    public class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteTicketHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.ticketRepository.GetByIdAsync(request.id);
            if(result == null)
            {

                return new Result<bool>
                {
                    Errors = new List<string> { "not found this ticket" },
                    IsSuccess = false,
                };
            }
            var res = await unitOfWork.ticketRepository.RemoveAsync(result);
            if (res)
                return new Result<bool>
                { IsSuccess = true};
            return new Result<bool>
            {
                Errors = new List<string> { "error delete" },
                IsSuccess = false
            };

        }
    }
}
