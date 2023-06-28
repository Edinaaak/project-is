using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Travel.Request
{
    public class TravelCreateRequest
    {
        
        public DateTime TravelDate { get; set; }
        public int ScheduleId { get; set; }
        public int BusId { get; set; }

    }
}
