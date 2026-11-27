using Personal_Fitness_Health_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Utility
{
    internal static class SessionHandler
    {
        public static User CurrentUser = new User
        {
            Id = Guid.NewGuid(),
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
