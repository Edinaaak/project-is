using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Bus.Response
{
    public class BusGetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Tv { get; set; }
        public bool AirConditioner { get; set; }
        public int SeatsNumber { get; set; }
        public string Year { get; set; }
    }
}
