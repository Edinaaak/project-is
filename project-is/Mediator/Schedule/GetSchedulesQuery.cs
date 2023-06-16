using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project_is.Mediator.Schedule
{
    public record GetSchedulesQuery : IRequest<Result<List<BusLine.Data.Models.Schedule>>>
    {
    }

    public class GetSchedulesHandler : IRequestHandler<GetSchedulesQuery, Result<List<BusLine.Data.Models.Schedule>>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetSchedulesHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BusLine.Data.Models.Schedule>>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.scheduleRepository.GetWithBusline();
            if (list == null)
                return new Result<List<BusLine.Data.Models.Schedule>>
                {
                    Errors = new List<string> { "List is empty"},
                    IsSuccess = false
                };
            return new Result<List<BusLine.Data.Models.Schedule>>
            {
                Data = list,
                IsSuccess = true
            };

        }
    }
}
