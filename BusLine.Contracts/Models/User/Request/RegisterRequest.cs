using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.User.Request
{
    public class RegisterRequest
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string WorkExperience { get; set; }
        public string EmploymentDate { get; set; }

        public string Role { get; set; }
    }
}
