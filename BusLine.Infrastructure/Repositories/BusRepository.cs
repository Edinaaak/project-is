using AutoMapper;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class BusRepository : Repository<Bus>, IBusRepository
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        public BusRepository(DataContext context, IMapper mapper, UserManager<User> userManager) : base(context, mapper)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<List<Bus>> getBusesForDriver(string IdDriver)
        {
            var travlelList = await context.travels.Include(x => x.Bus).Include(x => x.Schedule).Join(context.scheduleUsers.Where(x => x.UserId == IdDriver), t => t.Schedule.Id, d => d.ScheduleId, (t, d) => new { buses = t.Bus, id = d.Id}).ToListAsync();
            var busList = travlelList.Select( x=> x.buses).Distinct().ToList();
            return busList;

        }

        
    }
}
