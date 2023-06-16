using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.ScheduleUser
{
    public record UpdateScheduleUserCommand (ScheduleUserCreateRequest request, int id) : IRequest<Result<bool>>
    {
    }

    public class UpdateScheduleUserHandler : IRequestHandler<UpdateScheduleUserCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateScheduleUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateScheduleUserCommand request, CancellationToken cancellationToken)
        {
            var res = await unitOfWork.scheduleUserRepository.GetByIdAsync(request.id);
            mapper.Map<ScheduleUserCreateRequest, BusLine.Data.Models.ScheduleUser>(request.request, res);
            var result = await unitOfWork.CompleteAsync();
            if(result)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };
        }
    }
}
