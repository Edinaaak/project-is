using AutoMapper;
using BusLine.Data;
using BusLine.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public Repository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task AddAsync(T entity)
        {
             context.Set<T>().Add(entity);
        }

        public async Task<List<T>> GetAsync()
        {
           return await context.Set<T>().ToListAsync();   
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null)
                return null;
            return result;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            var result = await context.SaveChangesAsync();
            return result > 0;
            
        }

        public async Task<bool> UpdateAsync(T entity, int id)
        {
            var existEntity = await context.Set<T>().FindAsync(id);
            if (existEntity == null)
                return false;
            existEntity = mapper.Map<T>(entity);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
