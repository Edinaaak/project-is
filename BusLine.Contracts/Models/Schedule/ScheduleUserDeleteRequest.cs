using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule
{
    public class ScheduleUserDeleteRequest
    {
        public string IdUser { get; set; }
        public int IdSchedule { get; set; }
    }
}
