using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.ScheduleUser
{
    public record CreateScheduleUserCommand(ScheduleUserCreateRequest request) : IRequest<Result<bool>>
    {
    }

    public class CreateScheduleHandler : IRequestHandler<CreateScheduleUserCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(CreateScheduleUserCommand request, CancellationToken cancellationToken)
        {
            var scheduleUser = mapper.Map<BusLine.Data.Models.ScheduleUser>(request.request);
            await unitOfWork.scheduleUserRepository.AddAsync(scheduleUser);
            var result = await unitOfWork.CompleteAsync();
            if(result)
            {
                return new Result<bool>
                {
                    IsSuccess = true,
                };
            }
            return new Result<bool>
            {
                IsSuccess = false,
                Errors = new List<string> { "error" }
            };
        }
    }
}
