using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.User.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string WorkExperience { get; set; }
        public string EmploymentDate { get; set; }
        public IList<string> Role { get; set; }

        public string Error { get; set; }
    }
}
