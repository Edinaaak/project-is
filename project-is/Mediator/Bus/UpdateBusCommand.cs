using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Bus.Request;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Bus
{
    public record UpdateBusCommand (BusUpdateRequest request, int id) : IRequest<Result<BusLine.Data.Models.Bus>> 
    {
    }
    public class UpdateBusHandler : IRequestHandler<UpdateBusCommand, Result<BusLine.Data.Models.Bus>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateBusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<BusLine.Data.Models.Bus>> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
        {
            var mappedBus = await unitOfWork.busRepository.GetByIdAsync(request.id);
            mapper.Map<BusUpdateRequest, BusLine.Data.Models.Bus>(request.request, mappedBus);
            var updateUser = await unitOfWork.CompleteAsync();
            if (updateUser)
                return new Result<BusLine.Data.Models.Bus>
                {
                    IsSuccess = true,
                };
            return new Result<BusLine.Data.Models.Bus>
            {
                Errors = new List<string> { "Error in updating data" },
                IsSuccess = false,
            };
        }
    }
}
