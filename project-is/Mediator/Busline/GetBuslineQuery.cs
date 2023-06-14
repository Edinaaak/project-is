using BusLine.Contracts.Models;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Busline
{
    public record GetBuslineQuery (int id) : IRequest<Result<BusLine.Data.Models.BusLine>>
    {
    }

    public class GetBuslineHandler : IRequestHandler<GetBuslineQuery, Result<BusLine.Data.Models.BusLine>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetBuslineHandler(IUnitOfWork unitOfWork)
        {
                this.unitOfWork = unitOfWork;
        }
        public async Task<Result<BusLine.Data.Models.BusLine>> Handle(GetBuslineQuery request, CancellationToken cancellationToken)
        {
            var busline = await unitOfWork.buslineRepository.GetByIdAsync(request.id);
            if (busline == null)
                return new Result<BusLine.Data.Models.BusLine>
                {
                    Errors = new List<string> { "busline not found" },
                    IsSuccess = false
                };
            return new Result<BusLine.Data.Models.BusLine>
            { 
                Data = busline,
                IsSuccess= true
            };
            
        }
    }
}
