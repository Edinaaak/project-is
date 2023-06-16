using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Travel.Response
{
    public class TravelGetResponse
    {
        public DateTime TravelDate { get; set; }
        public BusLine.Data.Models.Schedule Schedule { get; set; }
    }
}
