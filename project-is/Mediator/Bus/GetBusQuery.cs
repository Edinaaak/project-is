using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Bus.Response;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Bus
{
    public record GetBusQuery (int id) : IRequest<Result<BusGetResponse>>
    {
    }
    public class GetOneBusHandler : IRequestHandler<GetBusQuery, Result<BusGetResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetOneBusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<BusGetResponse>> Handle(GetBusQuery request, CancellationToken cancellationToken)
        {
            var bus = await unitOfWork.busRepository.GetByIdAsync(request.id);
            var mappedBus = mapper.Map<BusGetResponse>(bus);
            if (bus == null)
                return new Result<BusGetResponse>
                {
                    Errors = new List<string> { "Bus not found" },
                    IsSuccess = false
                };
            return new Result<BusGetResponse>
            {
                Data = mappedBus,
                IsSuccess = true
            };
        }
    }
}
