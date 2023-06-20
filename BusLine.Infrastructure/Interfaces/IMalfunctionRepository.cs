using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface IMalfunctionRepository : IRepository<Malfunction>
    {
        Task<bool> ReportFault(MalfunctionCreateRequest request);
    }
}
