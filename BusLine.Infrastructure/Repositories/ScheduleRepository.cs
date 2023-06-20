using AutoMapper;
using BusLine.Contracts.Models.Schedule;
using BusLine.Contracts.Models.User.Response;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ScheduleRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Schedule>> GetWithBusline()
        {
            var list = await context.schedules.Include(x => x.BusLine).ToListAsync();
            return list;
        }

        public async Task<List<ScheduleUserResponse>> GetWithDrivers(int id)
        {
            var list = await context.scheduleUsers.Where(x => x.ScheduleId == id).Join(context.Users, x => x.UserId, u => u.Id, (x, u) => new { users = mapper.Map<UserResponse>(u) }).ToListAsync<object>();
            var listSchedule = await context.schedules.Where( x => x.Id == id).Select(x => new ScheduleUserResponse { Schedule = x, users = list }).ToListAsync();
            return listSchedule;
        }
    }
}
