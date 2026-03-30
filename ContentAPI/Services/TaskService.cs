using ContentAPI.DTOs;
using ContentAPI.Repositories;

namespace ContentAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskBreakdownResponse> CreateTaskBreakdownAsync(CreateTaskBreakdownRequest request)
        {
            var task = new Models.TaskBreakdown
            {
                GoalTitle = request.GoalTitle,
                Category = request.Category,
                Barriers = request.Barriers,
                ScareFactor = request.ScareFactor,
                MicroSteps = string.Empty, // Placeholder, to be updated later
                Encouragement = string.Empty, // Placeholder, to be updated later
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var createdTask = await _repository.CreateTaskBreakdownAsync(task);
            return new TaskBreakdownResponse(createdTask.Id, createdTask.GoalTitle, createdTask.Category, createdTask.Barriers, createdTask.ScareFactor, createdTask.MicroSteps, createdTask.Encouragement, createdTask.CreatedAt, createdTask.UpdatedAt);
        }

        public async Task<TaskBreakdownResponse?> GetTaskBreakdownByIdAsync(int id)
        {
            var task = await _repository.GetTaskBreakdownByIdAsync(id);
            if (task == null) return null;
            return new TaskBreakdownResponse(task.Id, task.GoalTitle, task.Category, task.Barriers, task.ScareFactor, task.MicroSteps, task.Encouragement, task.CreatedAt, task.UpdatedAt);
        }

        public async Task<IEnumerable<TaskBreakdownResponse>> GetAllTaskBreakdownsAsync(string? category, string? sortBy)
        {
            var tasks = await _repository.GetAllTaskBreakdownsAsync(category, sortBy);
            return tasks.Select(tasks => new TaskBreakdownResponse(tasks.Id, tasks.GoalTitle, tasks.Category, tasks.Barriers, tasks.ScareFactor, tasks.MicroSteps, tasks.Encouragement, tasks.CreatedAt, tasks.UpdatedAt));
        }

        public async Task<bool> UpdateTaskBreakdownAsync(int id, UpdateTaskBreakdownRequest request)
        {
            var existingTask = await _repository.GetTaskBreakdownByIdAsync(id);
            if (existingTask == null) return false;
            existingTask.GoalTitle = request.GoalTitle;
            existingTask.Category = request.Category;
            existingTask.Barriers = request.Barriers;
            existingTask.ScareFactor = request.ScareFactor;
            existingTask.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(existingTask);
            return true;
        }

        public async Task<bool> DeleteTaskBreakdownAsync(int id)
        {
            var existingTask = await _repository.GetTaskBreakdownByIdAsync(id);
            if (existingTask == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
