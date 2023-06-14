using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Busline.Request
{
    public class BuslineCreateRequest
    {

        public float Price { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime CreationDate { get; set; }
        public string Agency { get; set; }
    }
}
