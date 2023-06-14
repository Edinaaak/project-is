using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string WorkExperience { get; set; }
        public string EmploymentDate { get; set; }
        public List<ScheduleUser> ScheduleUsers { get; set; }
    }
}
