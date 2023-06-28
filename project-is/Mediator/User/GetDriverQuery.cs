using BusLine.Contracts.Models;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using BusLine.Infrastructure.Repositories;
using MediatR;

namespace project_is.Mediator.User
{
    public record GetDriverQuery : IRequest<Result<IList<BusLine.Data.Models.User>>>
    {
    }

    public class GetDriverHandler : IRequestHandler<GetDriverQuery, Result<IList<BusLine.Data.Models.User>>>
{
        private readonly IUnitOfWork unitOfWork;
        public GetDriverHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IList<BusLine.Data.Models.User>>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.userRepository.getDriver();
            return new Result<IList<BusLine.Data.Models.User>>
            {
                Data = list
            };
        }
    }
}