using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface IBusRepository : IRepository<Bus>
    {
        Task<List<Bus>> getBusesForDriver(string IdDriver);

       
    }
}
