using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule.Request
{
    public class ScheduleUpdateRequest
    {
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public int Platform { get; set; }
        public string Day { get; set; }
    }
}
