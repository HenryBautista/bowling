namespace Bowling.Interfaces;

using Bowling.Entities;

public interface IRepository<T>
{
    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<T> GetAllAsync();
    Task UpdateAsync(T entity);
}
