using AutoMapper;
using BusLine.Contracts.Models.Schedule;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class ScheduleUserRepository : Repository<ScheduleUser>, IScheduleUserRepository
    {
        private readonly DataContext context;

        public ScheduleUserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<bool> checkAvailability(ScheduUserCheckRequest request)
        {
            var user = await context.Users.Where(x => x.Id == request.IdDriver).FirstOrDefaultAsync();
            var list = await context.schedules.Join(context.scheduleUsers.Where(x => x.UserId == request.IdDriver), s => s.Id, su => su.ScheduleId, (s, su) => new ScheduleUser { schedule = s }).ToListAsync();
            foreach (var schedule in list)
            {
                if (schedule.schedule.Day == request.Day)
                    return false;
                
            }
            return true;
        }

        public async Task<bool> DeleteDriverFromSchedule(string driverId, int scheduleId)
        {
            var shUser = await context.scheduleUsers.Where(x=> x.UserId == driverId).Where( x => x.ScheduleId == scheduleId).FirstOrDefaultAsync();
            context.scheduleUsers.Remove(shUser);
            var result = await context.SaveChangesAsync();
            if(result > 0)
                return true;
            return false;

        }
    }
}
