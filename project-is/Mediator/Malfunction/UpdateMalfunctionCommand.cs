using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Malfunction
{
    public record UpdateMalfunctionCommand (MalfunctionCreateRequest request, int id) : IRequest<Result<bool>>
    {
    }

    public class UpdateMalfunctionHandler : IRequestHandler<UpdateMalfunctionCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateMalfunctionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateMalfunctionCommand request, CancellationToken cancellationToken)
        {
            var malfnt = await unitOfWork.malfunctionRepository.GetByIdAsync(request.id);
            mapper.Map<MalfunctionCreateRequest, BusLine.Data.Models.Malfunction>(request.request,malfnt);
            var res = await unitOfWork.CompleteAsync();
            if(res)
                return new Result<bool>()
                {
                   IsSuccess = true,


                };
            return new Result<bool>()
            {
                IsSuccess = false,
            };
        }
    }
}
