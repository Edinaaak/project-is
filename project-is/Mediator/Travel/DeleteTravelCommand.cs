using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Travel
{
    public record DeleteTravelCommand (int id) : IRequest<Result<bool>>
    {
    }

    public class DeleteTravelHandler : IRequestHandler<DeleteTravelCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteTravelHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteTravelCommand request, CancellationToken cancellationToken)
        {
            var travel = await unitOfWork.travelRepository.GetByIdAsync(request.id);
            if(travel == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "this travel does not exists" },
                    IsSuccess = false
                };
            }
            var result = await unitOfWork.travelRepository.RemoveAsync(travel);
            if(result)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };
        }
    }
}
