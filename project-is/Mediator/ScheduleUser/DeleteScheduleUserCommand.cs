using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.ScheduleUser
{
    public record DeleteScheduleUserCommand (ScheduleUserDeleteRequest request) : IRequest<Result<bool>>
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
            var result = await unitOfWork.scheduleUserRepository.DeleteDriverFromSchedule(request.request.IdUser, request.request.IdSchedule);
            if (!result)
                return new Result<bool>
                {
                    Errors = new List<string> { "this record can not delete" },
                    IsSuccess = false,
                };
            return new Result<bool> { IsSuccess = true };

        }
    }
}
