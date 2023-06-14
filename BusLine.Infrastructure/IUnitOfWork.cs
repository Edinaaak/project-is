using BusLine.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure
{
    public interface IUnitOfWork
    {
        IBuslineRepository buslineRepository { get; }
        IBusRepository busRepository { get; }
        IScheduleRepository scheduleRepository { get; }
        IScheduleUserRepository scheduleUserRepository { get; }
        ITicketRepository ticketRepository { get; }
        ITravelRepository travelRepository { get; }
        IUserRepository userRepository { get; }
        IMalfunctionRepository malfunctionRepository { get; }
        Task<bool> CompleteAsync();
    }
}
