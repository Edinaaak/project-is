using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Busline
{
    public record DeleteBuslineCommand (int id ): IRequest<Result<bool>>
    {
    }
    public class DeleteBuslineHandler : IRequestHandler<DeleteBuslineCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteBuslineHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteBuslineCommand request, CancellationToken cancellationToken)
        {
            var busline = await unitOfWork.buslineRepository.GetByIdAsync(request.id);
            if (busline == null)
                return new Result<bool>
                {
                    Errors = new List<string> { "user not found"},
                    IsSuccess = false
                };
            var res = await unitOfWork.buslineRepository.RemoveAsync(busline);
            if (res)
                return new Result<bool>
                { IsSuccess = true };
            return new Result<bool>
            {
                Errors = new List<string> { "error in deleting data" },
                IsSuccess = false
            };
            
        }
    }
}
