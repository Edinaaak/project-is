using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule.Request
{
    public class ScheduleDriverUpdateRequest
    {
        public ScheduleUpdateRequest request { get; set; }
         public string[]? DriverList { get; set; }
    }
}
