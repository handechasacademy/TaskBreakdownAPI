namespace ContentAPI.Models
{
    public class TaskBreakdown
    {
        public int Id { get; set; }
        public string GoalTitle { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Barriers { get; set; } = string.Empty;
        public int ScareFactor { get; set; }

        public string MicroSteps { get; set; } = string.Empty;
        public string Encouragement { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
