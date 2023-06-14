using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Bus.Response;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Bus
{
    public record GetBusesQuery : IRequest<Result<List<BusGetResponse>>>
    {
    }
    public class GetBusesHandler : IRequestHandler<GetBusesQuery, Result<List<BusGetResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetBusesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<List<BusGetResponse>>> Handle(GetBusesQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.busRepository.GetAsync();
            var mappedList = mapper.Map<List<BusGetResponse>>(lista);
            return new Result<List<BusGetResponse>> { Data = mappedList  };
        }
    }
}
