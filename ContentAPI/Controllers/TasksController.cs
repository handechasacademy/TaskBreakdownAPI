using ContentAPI.DTOs;
using ContentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskBreakdown([FromBody] CreateTaskBreakdownRequest request)
        {
            var result = await _taskService.CreateTaskBreakdownAsync(request);
            return CreatedAtAction(nameof(GetTaskBreakdownById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskBreakdownById(int id)
        {
            var result = await _taskService.GetTaskBreakdownByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTaskBreakdowns([FromQuery] string? category, [FromQuery] string? sortBy)
        {
            var result = await _taskService.GetAllTaskBreakdownsAsync(category, sortBy);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskBreakdown(int id, UpdateTaskBreakdownRequest request)
        {
            var result = await _taskService.UpdateTaskBreakdownAsync(id, request);
            if (result == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskBreakdown(int id)
        {
            var success = await _taskService.DeleteTaskBreakdownAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
