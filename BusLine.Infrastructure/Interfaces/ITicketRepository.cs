using BusLine.Contracts.Models.Ticket.Response;
using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<TicketReserveResponse> ReserveTicket(int numTicket, int idTravel);

        Task<int> GetFreeSeat(int travelId);

    }
}
