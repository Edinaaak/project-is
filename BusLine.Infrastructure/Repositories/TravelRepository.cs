using AutoMapper;
using BusLine.Contracts.Models.Schedule.Response;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class TravelRepository : Repository<Travel>, ITravelRepository
    {
        private readonly DataContext context;

        public TravelRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

            this.context = context;
        }

        public async Task<List<ScheduleResponse>> DriverSchedule(string id)
        {
            var list = await context.travels.Include(x => x.Schedule).Include(x => x.Schedule.BusLine).Join(context.scheduleUsers.Where(x => x.UserId == id), t => t.Schedule.Id, u => u.ScheduleId, (t, u) => new ScheduleResponse{ travel = t }).ToListAsync();
            return list;

        }

        public async Task<List<Travel>> GetTravelsByBus(int id)
        {
            var travelList = await context.travels.Where( x => x.BusId == id).ToListAsync();
            return travelList;
        }
    }
}