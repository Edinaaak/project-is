using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAsync();
        public Task<T> GetByIdAsync (int id);
        public Task<bool> RemoveAsync(T entity);
        public Task AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity, int id);

    }
}
