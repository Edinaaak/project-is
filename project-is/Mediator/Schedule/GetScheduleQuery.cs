using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace project_is.Mediator.Schedule
{
    public record GetScheduleQuery (int id) : IRequest<Result<BusLine.Data.Models.Schedule>>
    {
    }

    public class GetScheduleHandler : IRequestHandler<GetScheduleQuery, Result<BusLine.Data.Models.Schedule>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetScheduleHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<BusLine.Data.Models.Schedule>> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
        {
            var schedule = await unitOfWork.scheduleRepository.GetByIdAsync(request.id);
            if (schedule == null)
                return new Result<BusLine.Data.Models.Schedule>
                {
                    Errors = new List<string> { "Schedule does not exist"},
                    IsSuccess = false
                };
            return new Result<BusLine.Data.Models.Schedule>
            {
                Data = schedule,
                IsSuccess = true
            };

        }
    }
}
