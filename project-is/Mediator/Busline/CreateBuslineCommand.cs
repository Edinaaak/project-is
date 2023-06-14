using BusLine.Contracts.Models;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.Busline
{
    public record CreateBuslineCommand (BusLine.Data.Models.BusLine request) :IRequest<Result<BusLine.Data.Models.BusLine>> 
    {
    }

    public class CreateBuslineHandler : IRequestHandler<CreateBuslineCommand, Result<BusLine.Data.Models.BusLine>>
    {
        private readonly IUnitOfWork unitOfWork;
        public CreateBuslineHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<BusLine.Data.Models.BusLine>> Handle(CreateBuslineCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.buslineRepository.AddAsync(request.request);
            var result  = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<BusLine.Data.Models.BusLine>
                {
                    Errors = new List<string> { "can not create this busline"},
                    IsSuccess = false
                };
            return new Result<BusLine.Data.Models.BusLine>
            {
                IsSuccess = true
            };



        }
    }
}
