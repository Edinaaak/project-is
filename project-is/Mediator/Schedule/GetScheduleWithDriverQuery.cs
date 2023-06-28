using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Schedule
{
    public record GetScheduleWithDriverQuery (int id) : IRequest<Result<List<ScheduleUserResponse>>>
    {
    }
    public class GetScheduleWithDriverHandler : IRequestHandler<GetScheduleWithDriverQuery, Result<List<ScheduleUserResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetScheduleWithDriverHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ScheduleUserResponse>>> Handle(GetScheduleWithDriverQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.scheduleRepository.GetWithDrivers(request.id);
            return new Result<List<ScheduleUserResponse>>
            {
                Data = list,
                IsSuccess = true
            };
        }
    }
}
