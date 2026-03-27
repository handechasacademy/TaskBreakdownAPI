using ContentAPI.Data;
using ContentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskBreakdown> CreateTaskBreakdownAsync(TaskBreakdown taskBreakdown)
        {
            _context.TaskBreakdowns.Add(taskBreakdown);
            await _context.SaveChangesAsync();
            return taskBreakdown;
        }

        public async Task<TaskBreakdown?> GetTaskBreakdownByIdAsync(int id)
        {
            return await _context.TaskBreakdowns.FindAsync(id);
        }

        public async Task<IEnumerable<TaskBreakdown>> GetAllTaskBreakdownsAsync(string? category, string? sortBy)
        {
            var query = _context.TaskBreakdowns.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(t => t.Category == category);
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "createdat" => query.OrderBy(t => t.CreatedAt),
                    "updatedat" => query.OrderBy(t => t.UpdatedAt),
                    _ => query
                };
            }
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(TaskBreakdown task)
        {
            _context.TaskBreakdowns.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.TaskBreakdowns.FindAsync(id);
            if (task != null)
            {
                _context.TaskBreakdowns.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
