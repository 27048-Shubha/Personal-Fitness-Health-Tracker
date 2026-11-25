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

            decimal validationValue;
            if (activityDTO.Type == ActivityType.BodyWeight)
            {
                SessionHandler.CurrentUser.Weight = activityDTO.Value;
                validationValue = (SessionHandler.CurrentUser.Weight ?? activity.Value) / (SessionHandler.CurrentUser.Height * SessionHandler.CurrentUser.Height);
            }

            validationValue = activity.Value;

            return ActivityValidator.ValidateActivityRange(activity.Type, validationValue);
        }

        public bool ValidateActivityValue(ActivityType activityType, decimal activityValue)
        {
            switch (activityType)
            {
                case ActivityType.StepsWalked:
                    return (activityValue > 0);

                case ActivityType.Sleep:
                    return (activityValue >= 0 && activityValue <= 24);

                case ActivityType.WaterIntake:
                    return (activityValue >= 0 && activityValue <= 10);

                case ActivityType.BodyWeight:
                    return (activityValue >= 20 && activityValue <= 250);

                case ActivityType.RestingHeartRate:
                    return (activityValue > 20 && activityValue < 150);

                default:
                    return false;
            }
        }

        public List<GetActivityInfoDTO> Get(DateTime dateTime)
        {
            List < GetActivityInfoDTO >  logs = this.Get().FindAll(log => log.LoggedAt == DateTime.Today);
            Console.WriteLine(logs.Capacity);
            return logs;
        }

        public List<GetActivityInfoDTO> Get()
        {
            return this.MapToDTO(this._activityRepository.Get().FindAll(log => log.UserId == SessionHandler.CurrentUser.Id));
        }

        public List<GetActivityInfoDTO> MapToDTO(List<ActivityLog> logs)
        {
            if (logs == null)
            {
                return null;
            }

            List<GetActivityInfoDTO> activity = new();
            foreach(ActivityLog log in logs)
            {
                activity.Add(
                    new GetActivityInfoDTO
                    {
                        Name = log.Name,
                        LoggedAt = log.LoggedAt,
                        Type = log.Type,
                        Value = log.Value,
                    }
                );
            }

            return activity;
        }
    }
}
