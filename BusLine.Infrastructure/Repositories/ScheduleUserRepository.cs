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

        public async Task<string> checkAvailability(ScheduUserCheckRequest request)
        {
            var user = await context.Users.Where(x => x.Id == request.IdDriver).FirstOrDefaultAsync();
            var list = await context.schedules.Join(context.scheduleUsers.Where(x => x.UserId == request.IdDriver), s => s.Id, su => su.ScheduleId, (s, su) => new ScheduleUser { schedule = s }).ToListAsync();
            foreach (var schedule in list)
            {
                if  (schedule.schedule.Day == request.Day)
                    return $"Driver {user.Name} {user.Surname} is busy for this day";
                
            }
            return $"Driver {user.Name} {user.Surname} can drive for that day";
        }
    }
}
