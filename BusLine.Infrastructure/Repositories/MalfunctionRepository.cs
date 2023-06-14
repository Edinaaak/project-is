﻿using AutoMapper;
using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class MalfunctionRepository : Repository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}