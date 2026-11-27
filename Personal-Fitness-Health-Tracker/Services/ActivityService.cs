using Personal_Fitness_Health_Tracker.Enums;
using Personal_Fitness_Health_Tracker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Services
{
    using Personal_Fitness_Health_Tracker.Models;
    using Personal_Fitness_Health_Tracker.Models.DTOs;
    using Personal_Fitness_Health_Tracker.Repositories;

    internal class ActivityService
    {
        private readonly ActivityRepository _activityRepository;
        internal ActivityService(ActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
        }

        public HealthStatus RecordNew(AddActivityDTO activityDTO)
        {
            ActivityLog activity = new ActivityLog
            {
                Id = Guid.NewGuid(),
                UserId = activityDTO.UserId,
                Name = activityDTO.Name,
                LoggedAt = activityDTO.LoggedAt,
                Type = activityDTO.Type,
                Value = activityDTO.Value,
            };

            this._activityRepository.Add(activity);

            return ActivityValidator.ValidateActivityRange(activity.Type,activity.Value);
        }
    }
}
