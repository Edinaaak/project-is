using AutoMapper;
using BusLine.Contracts.Models;
using BusLine.Contracts.Models.User.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace project_is.Mediator.User
{
    public record GetUsersQuery : IRequest<Result<List<UserResponse>>>
    {
    }

    public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<List<UserResponse>>>
    {
        private readonly UserManager<BusLine.Data.Models.User> userManager;
        private readonly IMapper mapper;
        public GetUsersHandler(UserManager<BusLine.Data.Models.User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper= mapper;
        }

        public async Task<Result<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var index = 0;
            var list = await userManager.Users.ToListAsync();
            var mappedList = mapper.Map<List<UserResponse>>(list);
            foreach (var item in list)
            {
                mappedList[index].Role = userManager.GetRolesAsync(item).Result;
                index++;
                
            }
            return new Result<List<UserResponse>>
            {
                Data = mappedList,
                IsSuccess = true
            };
        }
    }
}
