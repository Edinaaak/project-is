using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using BusLine.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project_is.Mediator.Ticket
{
    public record GetSeatsQuery (int id) : IRequest<Result<int>> { }

    public class GetSeatsHandler : IRequestHandler<GetSeatsQuery, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetSeatsHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(GetSeatsQuery request, CancellationToken cancellationToken)
        {
            var num = await unitOfWork.ticketRepository.GetFreeSeat(request.id);
            return new Result<int>
            {
                Data = num
            };
        }
    }
}
