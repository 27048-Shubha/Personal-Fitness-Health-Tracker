using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Models
{
    internal class ActivityLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime LoggedAt { get; set; }
        public ActivityType Type { get; set; }
        public decimal Value { get; set; }

    }
}
