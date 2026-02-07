using TodoApi.Models;

namespace TodoApi.Services
{
    public interface IRepositoryTodo : IRepository<Todo>
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task AddAsync(Todo entity);
        void Update(Todo entity);
        void Delete(Todo entity);
        Task SaveAsync();
    }
}
