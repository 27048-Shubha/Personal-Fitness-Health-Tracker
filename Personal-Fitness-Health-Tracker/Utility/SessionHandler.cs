using Personal_Fitness_Health_Tracker.Models;

namespace Personal_Fitness_Health_Tracker.Utility
{
    internal static class SessionHandler
    {
        public static User CurrentUser = new User
        {
            Id = Guid.Parse("cbaa009a-5c3e-4117-acd6-5fbd57d2525d"),
            Username = "shubha",
            Password = "Password@123",
            FirstName = "Shubha",
            LastName = "Shree",
            DOB = DateTime.Now,
            Gender = 'F',
            Height = 170,
            Weight = 55,
        };
    }
}
