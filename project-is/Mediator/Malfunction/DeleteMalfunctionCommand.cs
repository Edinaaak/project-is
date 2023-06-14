using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Malfunction
{
    public record DeleteMalfunctionCommand (int id) : IRequest<Result<bool>>
    {
    }
    public class DeleteMalfunctionHandler : IRequestHandler<DeleteMalfunctionCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteMalfunctionHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteMalfunctionCommand request, CancellationToken cancellationToken)
        {
            var malfnc = await unitOfWork.malfunctionRepository.GetByIdAsync(request.id);
            var res = await unitOfWork.malfunctionRepository.RemoveAsync(malfnc);
            if(res)
                return new Result<bool> { IsSuccess= true };
            return new Result<bool> { IsSuccess= false };

        }
    }
}
