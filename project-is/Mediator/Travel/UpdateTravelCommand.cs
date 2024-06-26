﻿using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Travel.Request;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Travel
{
    public record UpdateTravelCommand (TravelUpdateRequest request, int id) : IRequest<Result<bool>>
    {
    }

    public class UpdateTravelHandler : IRequestHandler<UpdateTravelCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateTravelHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateTravelCommand request, CancellationToken cancellationToken)
        {
            var travel = await unitOfWork.travelRepository.GetByIdAsync(request.id);
            if (travel == null)
                return new Result<bool>
                {
                    Errors = new List<string> { "this travel is not found" },
                    IsSuccess = false,
                };
            var travelList = await unitOfWork.travelRepository.GetTravelsByBus(request.request.BusId);
            foreach(var travelInList in travelList)
            {
                if (travelInList.TravelDate == request.request.TravelDate)
                    return new Result<bool>
                    {
                        Errors = new List<string> { "This bus is busy for that date" },
                        IsSuccess = false,
                    };
            }
            mapper.Map<TravelUpdateRequest, BusLine.Data.Models.Travel>(request.request, travel);
            var res = await unitOfWork.CompleteAsync();
            if(res)
                return new Result<bool> { IsSuccess = true };
            return new Result<bool> { IsSuccess = false };
        }
    }
}
