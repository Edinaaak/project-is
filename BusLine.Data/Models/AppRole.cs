using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Data.Models
{
    public class AppRole : IdentityRole
    {
      
        public AppRole() { }

        public AppRole(string roleName) : base(roleName)
        {
        }
    }
}
