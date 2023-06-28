using BusLine.Contracts.Models;
using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Bus
{
    public record ReportFailureCommand (MalfunctionCreateRequest request) :  IRequest<Result<bool>> { }

    public class ReportFailureHandler : IRequestHandler<ReportFailureCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public ReportFailureHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ReportFailureCommand request, CancellationToken cancellationToken)
        {
            var report = await unitOfWork.malfunctionRepository.ReportFault(request.request);
            if (!report)
                return new Result<bool>
                {
                    Errors = new List<string> { "something went wrong, try again" },
                    IsSuccess = false
                };
            return new Result<bool>
            {
                IsSuccess = true
            };
        }
    }
}
