using BusLine.Contracts.Models;
using BusLine.Contracts.Models.User.Request;
using BusLine.Contracts.Models.User.Response;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using project_is.Exceptions;

namespace project_is.Mediator.User
{
    public record LoginUserCommand (LoginRequest Request) : IRequest<Result<LoginResponse>>
    {
    }

    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<LoginResponse>>
    {
        public IUnitOfWork unitOfWork;
        public LoginUserHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Request.Email) || string.IsNullOrEmpty(request.Request.Password))
                //throw new LoginCustomException("both input are required");
                return new Result<LoginResponse> { Errors = new List<string> { "Both inputs are required" } };
            var result = await unitOfWork.userRepository.login(request.Request);
            if (result.error != "")
            {
                return new Result<LoginResponse>
                {
                    Errors = new List<string> { result.error },
                    IsSuccess = false
                };

            }

            return new Result<LoginResponse>
            {
                Data = result,
                IsSuccess= true
            };

        }
    }
}
