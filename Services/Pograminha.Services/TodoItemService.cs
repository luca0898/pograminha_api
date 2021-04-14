using Pograminha.Domain.Contracts;
using Pograminha.Domain.Model;
using Pograminha.Infra.SQL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pograminha.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IUnitOfWorkFactory<UnitOfWork> _uow;

        public TodoItemService(ITodoItemRepository todoItemRepository, IUnitOfWorkFactory<UnitOfWork> uow)
        {
            _todoItemRepository = todoItemRepository;
            _uow = uow;
        }

        public async Task<TodoItem> CreateAsync(TodoItem entity)
        {
            using var uow = _uow.Create();

            var newlyEntity = await _todoItemRepository.CreateAsync(entity);

            await uow.CommitAsync();

            return newlyEntity;
        }
        public async Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            using var uow = _uow.Create();

            var updatedEntity = await _todoItemRepository.UpdateAsync(entity);

            await uow.CommitAsync();

            return updatedEntity;
        }

        public async Task DeleteAsync(TodoItem entity)
        {
            using var uow = _uow.Create();

            await _todoItemRepository.DeleteAsync(entity);

            await uow.CommitAsync();
        }

        public async Task<ICollection<TodoItem>> FindAllAsync()
        {
            return await _todoItemRepository.FindAllAsync();
        }

        public async Task<TodoItem> FindOneAsync(long id)
        {
            return await _todoItemRepository.FindOneAsync(id);
        }
    }
}
