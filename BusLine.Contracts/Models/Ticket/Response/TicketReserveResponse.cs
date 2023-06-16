using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Ticket.Response
{
    public class TicketReserveResponse
    {
        public List<int> seatNumbers { get; set; }
        public string Error { get; set; }
        public float Amount { get; set; }
    }
}
