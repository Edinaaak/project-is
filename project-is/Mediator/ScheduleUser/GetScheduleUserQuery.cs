using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project_is.Mediator.ScheduleUser
{
    public record GetScheduleUserQuery (int id) : IRequest<Result<BusLine.Data.Models.ScheduleUser>>
    {
    }

    public class GetScheduleUserHandler : IRequestHandler<GetScheduleUserQuery, Result<BusLine.Data.Models.ScheduleUser>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetScheduleUserHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<BusLine.Data.Models.ScheduleUser>> Handle(GetScheduleUserQuery request, CancellationToken cancellationToken)
        {
            var scheduleUser = await unitOfWork.scheduleUserRepository.GetByIdAsync(request.id);
            if (scheduleUser == null)
                return new Result<BusLine.Data.Models.ScheduleUser>
                {
                    Errors = new List<string> { "not found" },
                    IsSuccess = false
                };
            return new Result<BusLine.Data.Models.ScheduleUser>
            { Data = scheduleUser };
        }
    }
}
