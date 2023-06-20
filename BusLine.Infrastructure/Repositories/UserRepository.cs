using AutoMapper;
using BusLine.Contracts.Models.User.Request;
using BusLine.Contracts.Models.User.Response;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly ITravelRepository trravelRepository;
        private readonly IMapper mapper;
        public UserRepository(DataContext context, IMapper mapper, UserManager<User> userManager, ITravelRepository trravelRepository) : base(context, mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.trravelRepository = trravelRepository;
        }

       

        public async Task<IList<User>> getDriver()
        {
            var listDrivers = await userManager.GetUsersInRoleAsync("Driver");
            return listDrivers;
        }

        public async Task<LoginResponse> login(LoginRequest request)
        {

            var user = await userManager.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return new LoginResponse { error = "User with this email does not exist " };
            }

            if (await userManager.CheckPasswordAsync(user, request.Password))
            {
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTk"));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, request.Email)
                };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var mappedUser = mapper.Map<UserResponse>(user);
                var role = await userManager.GetRolesAsync(user);
                var schedule = await trravelRepository.DriverSchedule(user.Id);
                return new LoginResponse
                {
                    expires = DateTime.Now.AddHours(1),
                    token = toReturn,
                    user = mappedUser,
                    travels = schedule,
                    role = role,
                    error = ""
                };
            }
            else
            {
                return new LoginResponse { error = "Password is not correct" };
            }

        }

        public async Task<UserResponse> register(RegisterRequest request)
        {
            var existUser = await userManager.FindByEmailAsync(request.Email);
            if (existUser != null)
                return new UserResponse { Error = "User with this email already exists" };
            var user = mapper.Map<User>(request);
            var result = await userManager.CreateAsync(user, request.Password);
            if (request.Role == "2450eb67-a52c-417b-ba2b-f03236421190")
            {
                await userManager.AddToRoleAsync(user, "Driver");
            }
            else if (request.Role == "8f46d4ab-4491-4319-8086-e435464f1082")
            {
                await userManager.AddToRoleAsync(user, "Conductor");
            }
            if (!result.Succeeded)
            {
                return new UserResponse { Error = "can not save this user" };
            }
            var userMapped = mapper.Map<UserResponse>(user);
            return userMapped;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            string to = user.Email;
            string from = "softnalog@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.Name} {user.Surname}, <br> If you want to reset your password, click here! http://localhost:4200/change-password?Id={user.Id}&token={token}";
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential = new NetworkCredential("naprednebaze@gmail.com", "nwoouozmlgrmyqyr");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = credential;
            try
            {
                client.Send(message);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;

            }
        }
        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await userManager.FindByIdAsync(request.IdUser);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if(result.Succeeded)
                return true;
            return false;
        }
    }
}
