using BusLine.Contracts.Models;
using BusLine.Contracts.Models.User.Request;
using BusLine.Contracts.Models.User.Response;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project_is.Mediator.User 
{ 

    public record  RegisterUserCommand (RegisterRequest Request) : IRequest<Result<UserResponse>>
    {
    }
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        public RegisterUserHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<UserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.userRepository.register(request.Request);
            if (!string.IsNullOrEmpty(result.Error))
                return new Result<UserResponse>
                {
                    Errors = new List<string> { result.Error },
                    IsSuccess = false
                };
            if (result == null)
            {
                return new Result<UserResponse>
                {
                    Errors = new List<string> { "Something went wrong, try again" },
                    IsSuccess = false
                };

            }
            return new Result<UserResponse>
            {
                Data = result,
                IsSuccess= true
            };

        }
    }
}
