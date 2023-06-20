using BusLine.Contracts.Models.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule
{
    public class ScheduleUserResponse
    {
        public BusLine.Data.Models.Schedule Schedule { get; set; }
        public List<object> users { get; set; }
    }
}
