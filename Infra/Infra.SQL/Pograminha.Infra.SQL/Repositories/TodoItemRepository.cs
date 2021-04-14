using Microsoft.EntityFrameworkCore;
using Pograminha.Domain.Contracts;
using Pograminha.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pograminha.Infra.SQL.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        public ApplicationDbContext _dbContext;
        public DbSet<TodoItem> _todoItemsTable;

        public TodoItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _todoItemsTable = dbContext.Set<TodoItem>();
        }

        public async Task<TodoItem> CreateAsync(TodoItem entity)
        {
            var newlyEntity = await _todoItemsTable.AddAsync(entity);

            return newlyEntity.Entity;
        }
        public Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            var updatedEntity = _todoItemsTable.Update(entity);

            return Task.FromResult(updatedEntity.Entity);
        }

        public Task DeleteAsync(TodoItem entity)
        {
            _todoItemsTable.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<ICollection<TodoItem>> FindAllAsync()
        {
            return await _todoItemsTable.AsQueryable().ToListAsync();
        }

        public async Task<TodoItem> FindOneAsync(long id)
        {
            return await _todoItemsTable.FindAsync(new object[] { id });
        }
    }
}
