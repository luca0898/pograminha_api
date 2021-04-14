using Pograminha.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pograminha.Domain.Contracts
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> CreateAsync(TodoItem entity);
        Task<TodoItem> UpdateAsync(TodoItem entity);
        Task DeleteAsync(TodoItem entity);
        Task<TodoItem> FindOneAsync(long id);
        Task<ICollection<TodoItem>> FindAllAsync();
    }
}
