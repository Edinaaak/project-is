using BusLine.Contracts.Models.Schedule.Response;
using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface ITravelRepository : IRepository<Travel>
    {
        Task<List<ScheduleResponse>> DriverSchedule(string Id);
        Task<List<Travel>> GetTravelsByBus(int id);
    }
}
