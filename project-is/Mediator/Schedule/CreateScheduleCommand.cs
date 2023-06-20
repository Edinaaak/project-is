using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule;
using BusLine.Contracts.Models.Schedule.Request;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using BusLine.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace project_is.Mediator.Schedule
{
    public record CreateScheduleCommand (ScheduleCreateRequest request, string[] DriverList) : IRequest<Result<bool>>
    {
    }

    public class CreateScheduleHandler : IRequestHandler<CreateScheduleCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<BusLine.Data.Models.User> userManager;
        private readonly IMapper mapper;
        public CreateScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<BusLine.Data.Models.User> usermanager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userManager = usermanager;

        }
        public async Task<Result<bool>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            for (int i = 0; i < request.DriverList.Length; i++)
            {
                ScheduUserCheckRequest userCreateRequest = new ScheduUserCheckRequest();
                userCreateRequest.Day = request.request.Day;
                userCreateRequest.IdDriver = request.DriverList[i];
                var user = await userManager.FindByIdAsync(userCreateRequest.IdDriver);
                var response = await unitOfWork.scheduleUserRepository.checkAvailability(userCreateRequest);
                if (!response)
                    return new Result<bool>
                    {
                        Errors = new List<string> { $"Driver {user.Name} {user.Surname} can not join to this schedule because he is busy that day" },
                        IsSuccess = false
                    }; 
                
            };
            var schedule = mapper.Map<BusLine.Data.Models.Schedule>(request.request);
            await unitOfWork.scheduleRepository.AddAsync(schedule);
            var res = await unitOfWork.CompleteAsync();
            if(res)
            {
                for (int i = 0; i < request.DriverList.Length; i++) {
                    BusLine.Data.Models.ScheduleUser scheduleUser = new BusLine.Data.Models.ScheduleUser();
                    scheduleUser.ScheduleId = schedule.Id;
                    scheduleUser.UserId = request.DriverList[i];
                    await unitOfWork.scheduleUserRepository.AddAsync(scheduleUser);

                }
                var results = await unitOfWork.CompleteAsync();
                if (results)
                    return new Result<bool> { IsSuccess = true };
            }
            return new Result<bool> { IsSuccess = false };

        }
    }
}
