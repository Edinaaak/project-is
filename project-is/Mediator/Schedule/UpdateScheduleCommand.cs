using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Schedule.Request;
using BusLine.Data;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Schedule
{
    
    
    public record UpdateScheduleCommand(ScheduleUpdateRequest request, int id) : IRequest<Result<bool>>
    {
    }
    public class UpdateScheduleHandler : IRequestHandler<UpdateScheduleCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork= unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await unitOfWork.scheduleRepository.GetByIdAsync(request.id);
            mapper.Map<ScheduleUpdateRequest, BusLine.Data.Models.Schedule>(request.request, schedule);
            var result = await unitOfWork.CompleteAsync();
            if(result)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };
        }
    }
}
