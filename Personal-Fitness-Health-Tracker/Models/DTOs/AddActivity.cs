using Personal_Fitness_Health_Tracker.Enums;

namespace Personal_Fitness_Health_Tracker.Models.DTOs
{
    internal class AddActivityDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime LoggedAt { get; set; }
        public ActivityType Type { get; set; }
        public decimal Value { get; set; }
    }
}
