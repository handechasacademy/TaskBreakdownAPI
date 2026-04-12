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

        /// <summary>
        ///     Creates a new task breakdown using the specified request data.
        /// </summary>
        /// <remarks>The response includes a Location header with a URI to retrieve the created task
        /// breakdown by its identifier.</remarks>
        /// <param name="request">The details of the task breakdown to create. Must not be null.</param>
        /// <returns>An HTTP 201 Created response containing the created task breakdown if successful.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTaskBreakdown([FromBody] CreateTaskBreakdownRequest request)
        {
            var result = await _taskService.CreateTaskBreakdownAsync(request);
            return CreatedAtAction(nameof(GetTaskBreakdownById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Retrieves the breakdown details for a specific task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task for which to retrieve breakdown details.</param>
        /// <returns>An IActionResult containing the task breakdown details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskBreakdownById(int id)
        {
            var result = await _taskService.GetTaskBreakdownByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of task breakdowns, optionally filtered by category and sorted by the specified field.
        /// </summary>
        /// <param name="category">An optional category to filter the task breakdowns. If null, all categories are included.</param>
        /// <param name="sortBy">An optional field name to sort the results by. If null, the default sorting is applied.</param>
        /// <returns>An IActionResult containing a collection of task breakdowns that match the specified criteria.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTaskBreakdowns([FromQuery] string? category, [FromQuery] string? sortBy)
        {
            var result = await _taskService.GetAllTaskBreakdownsAsync(category, sortBy);
            return Ok(result);
        }

        /// <summary>
        /// Updates the breakdown details of an existing task with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="request">An object containing the updated breakdown information for the task. Cannot be null.</param>
        /// <returns>An IActionResult indicating the result of the update operation. Returns NoContent if the update is
        /// successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskBreakdown(int id, UpdateTaskBreakdownRequest request)
        {
            var result = await _taskService.UpdateTaskBreakdownAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes the task breakdown with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task breakdown to delete.</param>
        /// <returns>An IActionResult indicating the result of the operation. Returns NoContent if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskBreakdown(int id)
        {
            var success = await _taskService.DeleteTaskBreakdownAsync(id);
            return NoContent();
        }
    }
}
