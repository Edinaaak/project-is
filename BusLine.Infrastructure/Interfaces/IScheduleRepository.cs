﻿using BusLine.Contracts.Models.Schedule;
using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<List<Schedule>> GetWithBusline();

        Task<List<ScheduleUserResponse>> GetWithDrivers(int id);

       
    }
}
