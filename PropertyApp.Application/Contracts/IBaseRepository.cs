namespace PropertyApp.Application.Contracts;

public interface IBaseRepository<T, IdType> where T : class
{
    public Task<T> AddAsync(T entity);

    public Task DeleteAsync(T entity);


    public Task<IReadOnlyList<T>> GetAllAsync();


    public Task<T> GetByIdAsync(IdType id);


    public Task UpdateAsync(T entity);
   
}
