using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Bus
{
    public record DeleteBusCommand(int id ) : IRequest<Result<bool>>
    {
    }

    public class DeleteBusHandler : IRequestHandler<DeleteBusCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteBusHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            var bus = await unitOfWork.busRepository.GetByIdAsync(request.id);
            if (bus == null)
                return new Result<bool>
                {
                    Errors = new List<string> { "This bus does not exist" },
                    IsSuccess = false
                };
            var res = await unitOfWork.busRepository.RemoveAsync(bus);
            if (!res)
                return new Result<bool>
                {
                    Errors = new List<string> { "Error in removing this entity" },
                    IsSuccess = false
                };
            return new Result<bool>
            {
                IsSuccess = true
            };


        }
    }
}
