namespace Bowling.Interfaces;

using System.Collections.Generic;

public interface IRepository<T>
{
    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task UpdateAsync(T entity);
}
