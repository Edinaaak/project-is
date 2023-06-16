using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Bus.Request
{
    public class BusUpdateRequest
    {
        public string Name { get; set; }
        public bool Tv { get; set; }
        public bool AirConditioner { get; set; }
        public int SeatsNumber { get; set; }
    }
}
