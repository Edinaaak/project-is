using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Busline.Request;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Busline
{
    public record UpdateBuslineCommand (BuslineCreateRequest request, int id) : IRequest<Result<bool>>
    {
    }
    public class UpdateBuslineHandler : IRequestHandler<UpdateBuslineCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateBuslineHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateBuslineCommand request, CancellationToken cancellationToken)
        {
            var currBusline = await unitOfWork.buslineRepository.GetByIdAsync(request.id);
            mapper.Map<BuslineCreateRequest, BusLine.Data.Models.BusLine>(request.request, currBusline);
            var updated = await unitOfWork.CompleteAsync();
            if(updated)
                return new Result<bool>
                {
                    IsSuccess = true, 
                };
            else
                return new Result<bool> { IsSuccess = false,
                    Errors = new List<string> { "error in saving data" }

                };
        }
    }
}
