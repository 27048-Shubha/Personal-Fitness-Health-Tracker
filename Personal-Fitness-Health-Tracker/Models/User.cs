namespace Personal_Fitness_Health_Tracker.Models
{
    internal class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public char Gender { get; set; }
        public decimal Height { get; set; }
        public decimal? Weight { get; set; }
    }
}
