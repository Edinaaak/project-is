using BusLine.Contracts.Models.User.Request;
using BusLine.Contracts.Models.User.Response;
using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface IUserRepository  : IRepository<User>
    {
        public Task<UserResponse> register(RegisterRequest request);
        public Task<LoginResponse> login(LoginRequest request);
        public Task<IList<User>> getDriver();
    }
}
