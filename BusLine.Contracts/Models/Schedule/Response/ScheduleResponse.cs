using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule.Response
{
    public class ScheduleResponse
    {
        public BusLine.Data.Models.Travel travel { get; set; }
    }
}
