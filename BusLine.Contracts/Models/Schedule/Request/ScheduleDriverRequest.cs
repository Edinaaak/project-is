using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule.Request
{
    public class ScheduleDriverRequest
    {
        public ScheduleCreateRequest request { get; set; }
        public string[] DriverList { get; set; }
    }
}
