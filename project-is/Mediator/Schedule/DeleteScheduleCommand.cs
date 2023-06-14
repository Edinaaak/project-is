using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Schedule
{
    public record DeleteScheduleCommand (int id ): IRequest<Result<bool>>
    {
    }

    public class DeleteScheduleHandler : IRequestHandler<DeleteScheduleCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public async Task<Result<bool>> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var res = await unitOfWork.scheduleRepository.GetByIdAsync(request.id);
            if(res == null)
                return new Result<bool> {  Errors = new List<string> { "ok je"}, IsSuccess= false };
            var result = await unitOfWork.scheduleRepository.RemoveAsync(res);
            if (result)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { Errors = new List<string> { "greska prilikom brisanja" }, IsSuccess = false };
        }
    }
}
