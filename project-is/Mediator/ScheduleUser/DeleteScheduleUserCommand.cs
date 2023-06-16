using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.ScheduleUser
{
    public record DeleteScheduleUserCommand (int id) : IRequest<Result<bool>>
    {
    }

    public class DeleteScheduleUserHandler : IRequestHandler<DeleteScheduleUserCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteScheduleUserHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteScheduleUserCommand request, CancellationToken cancellationToken)
        {
            var scheduleUser = await unitOfWork.scheduleUserRepository.GetByIdAsync(request.id);
            if (scheduleUser == null)
                return new Result<bool>
                {
                    Errors = new List<string> { "this record does not exist" },
                    IsSuccess = false,
                };
            var result = await unitOfWork.scheduleUserRepository.RemoveAsync(scheduleUser);
            if (result)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };

        }
    }
}
