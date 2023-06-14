using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Bus.BusRequest
{
    public class BusCreateRequest
    {
        public string Name { get; set; }
        public bool Tv { get; set; }
        public bool AirConditioner { get; set; }
        public int SeatsNumber { get; set; }
        public string Year { get; set; }
    }
}
