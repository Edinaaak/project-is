using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Ticket.Request
{
    public class TicketReserveRequest
    {
        public int numTicket { get; set; }
        public int travelId { get; set; }
    }
}
