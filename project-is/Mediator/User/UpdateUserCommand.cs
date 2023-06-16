using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.User.Request;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace project_is.Mediator.User
{
    public record UpdateUserCommand (UserUpdateRequest request, string id) :  IRequest<Result<BusLine.Data.Models.User>>
    {
    }

    public class UdapteUserHandler : IRequestHandler<UpdateUserCommand, Result<BusLine.Data.Models.User>>
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

        public async Task<Result<BusLine.Data.Models.User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.id);
            if (user == null)
            {
                return new Result<BusLine.Data.Models.User>
                {
                    Errors = new List<string> { "user does not exist" },
                    IsSuccess = false,
                };
            }
            if (!string.IsNullOrEmpty(request.request.Password) && !string.IsNullOrEmpty(request.request.NewPassword))
            {
                await userManager.ChangePasswordAsync(user, request.request.Password, request.request.NewPassword);
            }
            mapper.Map<UserUpdateRequest, BusLine.Data.Models.User > (request.request, user);
            var res = await userManager.UpdateAsync(user);
            if(!res.Succeeded)
                return new Result<BusLine.Data.Models.User> { Errors = new List<string> { "error in saviing"}, IsSuccess = false, };
            return new Result<BusLine.Data.Models.User> { IsSuccess = true };
        }
    }
}
