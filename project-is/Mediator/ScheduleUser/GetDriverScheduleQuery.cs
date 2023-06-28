using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule;
using BusLine.Infrastructure;
using BusLine.Infrastructure.Repositories;
using MediatR;

namespace project_is.Mediator.ScheduleUser
{
    public record GetDriverScheduleQuery (ScheduUserCheckRequest request ) : IRequest<Result<bool>>
    {
    }

    public class GetDriverScheduleHandler : IRequestHandler<GetDriverScheduleQuery, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetDriverScheduleHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(GetDriverScheduleQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.scheduleUserRepository.checkAvailability(request.request);
            if (result)
                return new Result<bool>
                {
                    IsSuccess = true
                };
            return new Result<bool> { IsSuccess = false };
        }
    }
}
