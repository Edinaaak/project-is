using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Travel.Request;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Travel
{
    public record CreateTravelCommand (TravelCreateRequest request) : IRequest<Result<bool>>
    {
    }
    public class CreateTravelHandler : IRequestHandler<CreateTravelCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateTravelHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(CreateTravelCommand request, CancellationToken cancellationToken)
        {
            
            var travelList = await unitOfWork.travelRepository.GetTravelsByBus(request.request.BusId);
            foreach(var travelInList in travelList)
            {
                if (travelInList.TravelDate == request.request.TravelDate)
                    return new Result<bool>
                    {
                        Errors = new List<string> { "This bus is busy for that date" },
                        IsSuccess = false
                    };

            }
            var travel = mapper.Map<BusLine.Data.Models.Travel>(request.request);
            await unitOfWork.travelRepository.AddAsync(travel);
            var result = await unitOfWork.CompleteAsync();
            if(result)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };
        }
    }
}
