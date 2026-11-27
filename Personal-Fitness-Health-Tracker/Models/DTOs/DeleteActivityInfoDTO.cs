using Personal_Fitness_Health_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Models.DTOs
{
    internal class DeleteActivityInfoDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime LoggedAt { get; set; }
        public ActivityType Type { get; set; }
        public decimal Value { get; set; }

        public decimal Health { get; set; }
    }
}
