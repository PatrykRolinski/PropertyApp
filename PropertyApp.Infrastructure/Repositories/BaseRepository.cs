using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Infrastructure.Repositories;

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

    public async Task<T> GetByIdAsync(IdType id)
    {
        // NOTE: Is Exceptions good Idea in repository?
        var result= await _context.Set<T>().FindAsync(id);
       // if (result == null) throw new NotFoundException($"Item with {id} not found");
        return result;
    }

    public async Task UpdateAsync(T entity)
    {
        
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
    public  IQueryable<T> GetAllQuery()
    {
        return  _context.Set<T>().AsQueryable();
    }
}
