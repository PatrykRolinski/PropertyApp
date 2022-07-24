using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Infrastructure.Repositories
{
    public class BaseRepository<T, IdType> : IBaseRepository<T, IdType> where T : class
    {
        protected readonly PropertyAppContext _context;

        public BaseRepository(PropertyAppContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           await  _context.SaveChangesAsync();
           return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           var result = await _context.Set<T>().ToListAsync();
            return result;
        }

        public Task<T> GetByIdAsync(IdType id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
