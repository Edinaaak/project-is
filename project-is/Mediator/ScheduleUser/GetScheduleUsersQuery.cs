using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.ScheduleUser
{
    public record GetScheduleUsersQuery : IRequest<Result<List<BusLine.Data.Models.ScheduleUser>>>
    {
    }

    public class GetScheduleUsersHandler : IRequestHandler<GetScheduleUsersQuery, Result<List<BusLine.Data.Models.ScheduleUser>>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetScheduleUsersHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BusLine.Data.Models.ScheduleUser>>> Handle(GetScheduleUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.scheduleUserRepository.GetAsync();
            return new Result<List<BusLine.Data.Models.ScheduleUser>> { Data = list, IsSuccess = true };
        }
    }
}
