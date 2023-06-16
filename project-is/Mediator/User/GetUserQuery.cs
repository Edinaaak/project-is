using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.User.Response;
using BusLine.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace project_is.Mediator.User
{
    public record GetUserQuery (string id) : IRequest<Result<UserResponse>>
    {
    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, Result<UserResponse>>
    {
        private readonly UserManager<BusLine.Data.Models.User> userManager;
        private readonly IMapper mapper;
     
        public GetUserHandler(UserManager<BusLine.Data.Models.User> userManager, IMapper mapper) 
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.id);
            if(user == null)
                return new Result<UserResponse> { Errors = new List<string> {"user does not exist" }, IsSuccess = false };
            var mappedUser = mapper.Map<UserResponse>(user);
            return new Result<UserResponse> {Data = mappedUser, IsSuccess = true};
        }
    }
}
