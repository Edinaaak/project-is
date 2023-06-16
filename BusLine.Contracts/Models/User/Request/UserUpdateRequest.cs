using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.User.Request
{
    public class UserUpdateRequest
    {
        public string? Password { get; set; }
        public  string? NewPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmploymentDate { get; set; }
    }
}
