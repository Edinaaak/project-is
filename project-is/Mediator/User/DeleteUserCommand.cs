using BusLine.Contracts.Models;
using BusLine.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project_is.Mediator.User
{
    public record DeleteUserCommand (string id) : IRequest<Result<bool>>
    {
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly UserManager<BusLine.Data.Models.User> userManager;
        private readonly IUnitOfWork unitOfWork;
        public DeleteUserHandler(UserManager<BusLine.Data.Models.User> userManager,IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.id);
            if (user == null)
                return new Result<bool>
                {
                    Errors = new List<string> { "user does not exist" },
                    IsSuccess = true,
                };
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return new Result<bool> { Errors = new List<string> { "erron in saving data " }, IsSuccess = false };
            return new Result<bool> { IsSuccess = true };

        }
    }
}
