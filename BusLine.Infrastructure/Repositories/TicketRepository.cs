using AutoMapper;
using BusLine.Contracts.Models.Ticket.Response;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly DataContext context;
        public TicketRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<int> GetFreeSeat(int travelId)
        {
            var lastTicket = await context.tickets.OrderByDescending(e => e.Id).Where(x => x.TravelId == travelId).FirstOrDefaultAsync();
            var travel =  await context.travels.Include(x => x.Schedule).Include(x => x.Schedule.BusLine).Include(x => x.Bus).Where(x => x.Id == travelId).FirstOrDefaultAsync();
            if (lastTicket == null)
                return travel.Bus.SeatsNumber;
            var freeSeats = travel.Bus.SeatsNumber - lastTicket.SeatNumber;
            if(freeSeats > 0)
            {
                return freeSeats;
            }
            return 0;
        }

        public async Task<TicketReserveResponse> ReserveTicket(int numTicket, int idTravel)
        {
            var lastReservedSeat = 0;
            var travel = await context.travels.Include(x => x.Schedule).Include(x => x.Schedule.BusLine).Include(x => x.Bus).Where(x => x.Id == idTravel).FirstOrDefaultAsync();
            if(travel == null)
            {
                return new TicketReserveResponse { Error = "this travel does not exist" };
            }
            var seatNum = travel.Bus.SeatsNumber;
            var price = travel.Schedule.BusLine.Price;
            var lastTicket = await context.tickets.OrderByDescending(e => e.Id).Where(x => x.TravelId == idTravel).FirstOrDefaultAsync();
            if (lastTicket == null)
                lastReservedSeat = 0;
            else
            {
                lastReservedSeat = lastTicket.SeatNumber;
                if (seatNum == lastReservedSeat)
                    return new TicketReserveResponse { Error = "There are no more tickets for this travel" };
            }
            if(numTicket == 1)
            {
                Ticket ticket = new Ticket();
                ticket.SeatNumber = ++lastReservedSeat;
                ticket.TravelId = idTravel; 
                ticket.Created = DateTime.Now;
                await context.tickets.AddAsync(ticket);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                    return new TicketReserveResponse { Amount = price, seatNumbers = new List<int> { ticket.SeatNumber } };
                else return new TicketReserveResponse { Error = "Error in adding data" };
            }
            else
            {
                TicketReserveResponse ticketReserveResponse = new TicketReserveResponse();
                ticketReserveResponse.seatNumbers = new List<int>();
                if(lastReservedSeat + numTicket > seatNum)
                {
                    numTicket = seatNum  - lastReservedSeat;

                }
                for(int i = 0; i < numTicket; i++)
                {
                    Ticket reservation = new Ticket();
                    reservation.SeatNumber = ++lastReservedSeat;
                    reservation.TravelId = idTravel;
                    reservation.Created = DateTime.Now;
                    await context.tickets.AddAsync(reservation);
                    ticketReserveResponse.seatNumbers.Add(reservation.SeatNumber);
                    lastReservedSeat = reservation.SeatNumber;
                }
                ticketReserveResponse.Amount = numTicket * price;
                var result = await context.SaveChangesAsync();
                if (result == numTicket)
                    return ticketReserveResponse;
                return new TicketReserveResponse { Error = "Error in saivng data" };
            }

           
        }
    }
}
