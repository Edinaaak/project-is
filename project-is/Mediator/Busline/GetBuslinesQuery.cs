using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Busline
{
    public record GetBuslinesQuery : IRequest<Result<List<BusLine.Data.Models.BusLine>>>
    {
    }

    public class GetBuslinesHandler : IRequestHandler<GetBuslinesQuery, Result<List<BusLine.Data.Models.BusLine>>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetBuslinesHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BusLine.Data.Models.BusLine>>> Handle(GetBuslinesQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.buslineRepository.GetAsync();
            return new Result<List<BusLine.Data.Models.BusLine>>()
            {
               Data = lista,
               IsSuccess = true
            };
        }
    }
}
