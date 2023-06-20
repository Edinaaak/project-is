using BusLine.Contracts.Models.Schedule;
using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface IScheduleUserRepository : IRepository<ScheduleUser>
    {
        Task<bool> checkAvailability(ScheduUserCheckRequest request);

        Task<bool> DeleteDriverFromSchedule(string driverId, int scheduleId);
    }
}
