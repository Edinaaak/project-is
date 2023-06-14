using AutoMapper;
using BusLine.Data;
using BusLine.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class BuslineRepository : Repository<Data.Models.BusLine>, IBuslineRepository
    {
        public BuslineRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
