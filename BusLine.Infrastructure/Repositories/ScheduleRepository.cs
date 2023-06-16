using AutoMapper;
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
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly DataContext context;
        public ScheduleRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<List<Schedule>> GetWithBusline()
        {
            var list = await context.schedules.Include(x => x.BusLine).ToListAsync();
            return list;
        }
    }
}
