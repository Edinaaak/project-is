using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project_is.Mediator.Malfunction
{
    public record CreateMalfunctionCommand (MalfunctionCreateRequest request) : IRequest<Result<bool>>
    {
    }
    public class CreateMalfunctionHandler : IRequestHandler<CreateMalfunctionCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateMalfunctionHandler(IUnitOfWork unitOfWork, IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(CreateMalfunctionCommand request, CancellationToken cancellationToken)
        {
            var malfnc = mapper.Map<BusLine.Data.Models.Malfunction>(request.request);
            await unitOfWork.malfunctionRepository.AddAsync(malfnc);
            var res = await unitOfWork.CompleteAsync();
            if(res)
                return new Result<bool>
                { IsSuccess= true };
            return new Result<bool>
            {
                IsSuccess = false,
                Errors = new List<string> { "error in saving data" }
            };

        }
    }
}
