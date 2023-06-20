using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.User
{
    public record ForgotPasswordCommand (string email) : IRequest<Result<bool>> { }

    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public ForgotPasswordHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.userRepository.ForgotPassword(request.email);
            if (!result)
                return new Result<bool>
                {
                    Errors = new List<string> { "something went wrong, try again" },
                    IsSuccess = false
                };
            return new Result<bool>
            { IsSuccess= true };
            
        }
    }
}
