using BusLine.Data;
using BusLine.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        public UnitOfWork(DataContext context, IBuslineRepository buslineRepository, IBusRepository busRepository, IScheduleRepository scheduleRepository, IScheduleUserRepository scheduleUserRepository, ITicketRepository ticketRepository, ITravelRepository travelRepository, IUserRepository userRepository, IMalfunctionRepository malfunctionRepository)
        {
            this.context = context;
            this.buslineRepository = buslineRepository;
            this.busRepository = busRepository;
            this.scheduleRepository = scheduleRepository;
            this.scheduleUserRepository = scheduleUserRepository;
            this.ticketRepository = ticketRepository;
            this.travelRepository = travelRepository;
            this.userRepository = userRepository;
            this.malfunctionRepository = malfunctionRepository;
        }

        public IBuslineRepository buslineRepository { get; }
        public IBusRepository busRepository { get; }
        public IScheduleRepository scheduleRepository { get; }
        public IScheduleUserRepository scheduleUserRepository { get; }
        public ITicketRepository ticketRepository { get; }
        public ITravelRepository travelRepository { get; }
        public IUserRepository userRepository { get; }
        public IMalfunctionRepository malfunctionRepository { get; }

        public async Task<bool> CompleteAsync()
        {
           var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
