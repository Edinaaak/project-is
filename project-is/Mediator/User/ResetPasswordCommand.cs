using BusLine.Contracts.Models;
using BusLine.Contracts.Models.User.Request;
using BusLine.Infrastructure;
using MediatR;

namespace project_is.Mediator.User
{
    public record ResetPasswordCommand (ResetPasswordRequest request) : IRequest<Result<bool>>
    {
    }

    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public ResetPasswordHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.userRepository.ResetPassword(request.request);
            if (!result)
                return new Result<bool>
                {
                    Errors = new List<string> { "Can not reset password, try later" },
                    IsSuccess = false
                };
            return new Result<bool> { IsSuccess= true };
               
        }
    }
}
