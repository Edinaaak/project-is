using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.User.Request;
using BusLine.Contracts.Models.User.Response;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace project_is.Mediator.User
{
    public record UpdateUserCommand (UserUpdateRequest request, string id) :  IRequest<Result<UserResponse>>
    {
    }

    public class UdapteUserHandler : IRequestHandler<UpdateUserCommand, Result<UserResponse>>
    {
        private readonly UserManager<BusLine.Data.Models.User> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UdapteUserHandler(UserManager<BusLine.Data.Models.User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.id);
            if (user == null)
            {
                return new Result<UserResponse>
                {
                    Errors = new List<string> { "user does not exist" },
                    IsSuccess = false,
                };
            }
            if (!string.IsNullOrEmpty(request.request.Password) && !string.IsNullOrEmpty(request.request.NewPassword))
            {
               var result =  await userManager.ChangePasswordAsync(user, request.request.Password, request.request.NewPassword);
                if (!result.Succeeded)
                    return new Result<UserResponse>
                    {
                        Errors = new List<string> { "Your current password isn't correct" },
                        IsSuccess = false,
                    };
            }
            mapper.Map<UserUpdateRequest, BusLine.Data.Models.User > (request.request, user);
            var res = await userManager.UpdateAsync(user);
            if(!res.Succeeded)
                return new Result<UserResponse> { Errors = new List<string> { "error in saviing"}, IsSuccess = false, };
            return new Result<UserResponse> {
                Data = mapper.Map<UserResponse>(user),
                IsSuccess = true };
        }
    }
}
