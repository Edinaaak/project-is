using BusLine.Contracts.Models.Schedule.Response;
using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.User.Response
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public DateTime expires { get; set; }
        public string token { get; set; }

        public UserResponse user { get; set; }
        public List<ScheduleResponse>? travels { get; set; }   
        public IList<string> role { get; set; }
        public string error { get; set; }

    }
}
