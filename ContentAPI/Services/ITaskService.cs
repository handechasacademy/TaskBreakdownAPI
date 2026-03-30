using ContentAPI.DTOs;
using ContentAPI.Models;
using ContentAPI.Repositories;

namespace ContentAPI.Services
{
    public interface ITaskService
    {
        Task<TaskBreakdownResponse> CreateTaskBreakdownAsync(CreateTaskBreakdownRequest request);
        Task<TaskBreakdownResponse?> GetTaskBreakdownByIdAsync(int id);
        Task<IEnumerable<TaskBreakdownResponse>> GetAllTaskBreakdownsAsync(string? category, string? sortBy);
        Task<bool> UpdateTaskBreakdownAsync(int id, UpdateTaskBreakdownRequest request);
        Task<bool> DeleteTaskBreakdownAsync(int id);

    }
}
