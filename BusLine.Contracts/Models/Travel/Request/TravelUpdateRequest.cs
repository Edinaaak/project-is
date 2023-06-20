using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Travel.Request
{
    public class TravelUpdateRequest
    {
        public DateTime TravelDate { get; set; }
        public int BusId { get; set; }
    }
}
