﻿using BusLine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface IBuslineRepository : IRepository<BusLine.Data.Models.BusLine>
    {
    }
}
