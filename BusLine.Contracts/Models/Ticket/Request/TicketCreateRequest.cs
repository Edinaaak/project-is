using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Ticket.Request
{
    public class TicketCreateRequest
    {
        public DateTime Created { get; set; }
        public int SeatNumber { get; set; }
        public int TravelId { get; set; }
    }
}
