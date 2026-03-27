using ContentAPI.Models;

namespace ContentAPI.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskBreakdown> CreateTaskBreakdownAsync(TaskBreakdown taskBreakdown);
        Task<TaskBreakdown?> GetTaskBreakdownByIdAsync(int id);
        Task<IEnumerable<TaskBreakdown>> GetAllTaskBreakdownsAsync(string? category, string? sortBy);
        Task UpdateAsync(TaskBreakdown task);
        Task DeleteAsync(int id);
    }
}
