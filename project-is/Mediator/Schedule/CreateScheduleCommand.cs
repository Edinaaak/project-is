using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule.Request;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using BusLine.Infrastructure.Repositories;
using MediatR;

namespace project_is.Mediator.Schedule
{
    public record CreateScheduleCommand (ScheduleCreateRequest request) : IRequest<Result<bool>>
    {
    }

    public class CreateScheduleHandler : IRequestHandler<CreateScheduleCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<BusLine.Data.Models.Schedule>(request.request);
            BusLine.Data.Models.ScheduleUser scheduleUser = new BusLine.Data.Models.ScheduleUser();
            await unitOfWork.scheduleRepository.AddAsync(result);
            var res = await unitOfWork.CompleteAsync();
            if (res)
            {
                scheduleUser.ScheduleId = result.Id;
                scheduleUser.UserId = request.request.DriverId;
                await unitOfWork.scheduleUserRepository.AddAsync(scheduleUser);
                await unitOfWork.CompleteAsync();
                return new Result<bool>
                {
                    IsSuccess = true,
                };

               
            }
            return new Result<bool> { IsSuccess = false };
        }
    }
}
