using BusLine.Contracts.Models;
using BusLine.Data.Models;
using MediatR;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Bus.BusRequest;
using BusLine.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BusLine.Infrastructure;
using AutoMapper;

namespace project_is.Mediator.Bus
{
    public record CreateBusCommand (BusCreateRequest request) : IRequest<Result<BusLine.Data.Models.Bus>>
    {
    }

    public class CreateBusHandler : IRequestHandler<CreateBusCommand, Result<BusLine.Data.Models.Bus>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateBusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<BusLine.Data.Models.Bus>> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {
            var busMapped = mapper.Map<BusLine.Data.Models.Bus>(request.request);
            await unitOfWork.busRepository.AddAsync(busMapped);
            var res = await unitOfWork.CompleteAsync();
            if (res)
            {

                return new Result<BusLine.Data.Models.Bus>
                {
                    Data = busMapped,
                    IsSuccess = true
                };
            }
            
            return new Result<BusLine.Data.Models.Bus>
            {
                Errors = new List<string> { "Error in saving data" },
                IsSuccess = false
            };

        }
    }
}
