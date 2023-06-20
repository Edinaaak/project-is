using AutoMapper;
using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusLine.Infrastructure.Repositories
{
    public class MalfunctionRepository : Repository<Malfunction>, IMalfunctionRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public MalfunctionRepository(DataContext context, IMapper mapper, UserManager<User> userManager) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<bool> ReportFault(MalfunctionCreateRequest request)
        {
            Bus bus = await context.buses.Where(x => x.Id == request.BusId).FirstOrDefaultAsync();
            var malfucntion = mapper.Map<Malfunction>(request);
            await context.malfunctions.AddAsync(malfucntion);
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
            var to = await userManager.GetUsersInRoleAsync("Admin");
            var user = to.FirstOrDefault();
            string from = "softnalog@gmail.com";
            MailMessage message = new MailMessage(from, "edinakucevic26@gmail.com");
            string mailBody = $"Hi {user.Name}, <br>" + Environment.NewLine + $"as an administrator, you need to know that the bus (name - {bus.Name}) has a failure, date: {request.Date}, and that the description of that failure is :{request.Description} , so you need to update the new car for the timetable";
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential networkCredential = new NetworkCredential("naprednebaze@gmail.com", "nwoouozmlgrmyqyr");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = networkCredential;
            try
                {
                    client.Send(message);
                }
            catch (Exception ex)
                { throw ex; }

            return true;
            }
            return false;
        }
    }
}
