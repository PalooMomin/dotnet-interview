using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
namespace TodoApi.Services
{
    public class RepositoryTodo : Repository<Todo>, IRepositoryTodo
    {
        public RepositoryTodo(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Todo>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<Todo?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task AddAsync(Todo entity)
            => await _dbSet.AddAsync(entity);

        public void Update(Todo entity)
            => _dbSet.Update(entity);

        public void Delete(Todo entity)
            => _dbSet.Remove(entity);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
