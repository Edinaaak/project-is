using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.User.Request
{
    public class ResetPasswordRequest
    {
        public string IdUser { get; set; }
        public string NewPassword { get; set; }
    }
}
