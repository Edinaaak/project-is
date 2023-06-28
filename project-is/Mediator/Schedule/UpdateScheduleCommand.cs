using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule;
using BusLine.Contracts.Models.Schedule.Request;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace project_is.Mediator.Schedule
{
    
    
    public record UpdateScheduleCommand(ScheduleUpdateRequest request, string[] DriverList, int id) : IRequest<Result<bool>>
    {
    }
    public class UpdateScheduleHandler : IRequestHandler<UpdateScheduleCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<BusLine.Data.Models.User> userManager;
        private readonly IMapper mapper;
        public UpdateScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<BusLine.Data.Models.User> userManager)
        {
            this.unitOfWork= unitOfWork;
            this.mapper = mapper;
            this.userManager= userManager;
        }

        public async Task<Result<bool>> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {   
            var schedule = await unitOfWork.scheduleRepository.GetByIdAsync(request.id);
            mapper.Map<ScheduleUpdateRequest, BusLine.Data.Models.Schedule>(request.request, schedule);
            if (request.DriverList.Length != 0)
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
                            Errors = new List<string> { $"Driver {user.Name} {user.Surname} can not join to this schedule" },
                            IsSuccess = false
                        };
                    BusLine.Data.Models.ScheduleUser scheduleUser = new BusLine.Data.Models.ScheduleUser();
                    scheduleUser.ScheduleId = schedule.Id;
                    scheduleUser.UserId = request.DriverList[i];
                    await unitOfWork.scheduleUserRepository.AddAsync(scheduleUser);
                    var results = await unitOfWork.CompleteAsync();
                    if (results)
                        return new Result<bool> { IsSuccess = true };
                    return new Result<bool> { IsSuccess = false };


                };
            }
           
            var res = await unitOfWork.scheduleRepository.UpdateAsync(schedule, request.id);
            if (res)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };
        }
    }
}
