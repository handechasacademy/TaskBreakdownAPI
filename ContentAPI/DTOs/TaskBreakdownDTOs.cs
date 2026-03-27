using System.ComponentModel.DataAnnotations;

namespace ContentAPI.DTOs
{
    public record TaskBreakdownResponse(
        int Id,
        string GoalTitle,
        string Category,
        string Barriers,
        int ScareFactor,
        string MicroSteps,
        string Encouragement,
        DateTime CreatedAt,
        DateTime UpdatedAt
        );

    public record CreateTaskBreakdownRequest(
        [Required(ErrorMessage= "GoalTitle is required.")][StringLength(300, MinimumLength = 2, ErrorMessage = "GoalTitle needs to be between 2 and 300 characters")] string GoalTitle,
        [Required(ErrorMessage= "Category is required.")] [StringLength(100, MinimumLength = 1, ErrorMessage = "Category needs to be between 1 and 100 characters.")] string Category,
        [Required(ErrorMessage="Barrier is required for better help")] [StringLength(500, MinimumLength = 1, ErrorMessage= "Barrier needs to be between 1 and 500 characters.")] string Barriers,
        [Range(1, 10)] int ScareFactor
        );

    public record UpdateTaskBreakdownRequest(
        [Required(ErrorMessage= "GoalTitle is required.")][StringLength(300, MinimumLength = 2, ErrorMessage ="GoalTitle needs to be longer than 2 characters and shorter than 300 characters.")] string GoalTitle,
        string Category,
        string Barriers,
        int ScareFactor
        );
}
