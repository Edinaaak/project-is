using BusLine.Contracts.Models;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Globalization;

namespace project_is.Mediator.Bus
{
    public record getBusesForDriverQuery (string id) : IRequest<Result<List<BusLine.Data.Models.Bus>>>
    {
    }

    public class getBusesForDriverHandler : IRequestHandler<getBusesForDriverQuery, Result<List<BusLine.Data.Models.Bus>>>
    {
        private readonly IUnitOfWork unitOfWork;
        public getBusesForDriverHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BusLine.Data.Models.Bus>>> Handle(getBusesForDriverQuery request, CancellationToken cancellationToken)
        {
            var busList = await unitOfWork.busRepository.getBusesForDriver(request.id);
            return new Result<List<BusLine.Data.Models.Bus>>
            {
                Data = busList,
                IsSuccess = true,
            };
        }
    }
}
