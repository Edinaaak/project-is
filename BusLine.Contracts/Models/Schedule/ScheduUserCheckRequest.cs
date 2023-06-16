using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule
{
    public class ScheduUserCheckRequest
    {
        public string IdDriver { get; set; }
        public string Day { get; set; }
    }
}
