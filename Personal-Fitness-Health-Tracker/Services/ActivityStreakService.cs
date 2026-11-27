using Personal_Fitness_Health_Tracker.Enums;
using Personal_Fitness_Health_Tracker.Models;
using Personal_Fitness_Health_Tracker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Services
{
    internal class ActivityStreakService
    {
        public Dictionary<ActivityType, int> GetStreakInfo(List<ActivityLog> logs)
        {
            Dictionary<ActivityType, int> streakInfo = new();
            streakInfo.Add(ActivityType.StepsWalked, this.CalculateIndividualActivitytreak(logs, ActivityType.StepsWalked));
            streakInfo.Add(ActivityType.WaterIntake, this.CalculateIndividualActivitytreak(logs, ActivityType.WaterIntake));
            streakInfo.Add(ActivityType.BodyWeight, this.CalculateIndividualActivitytreak(logs, ActivityType.BodyWeight));
            streakInfo.Add(ActivityType.Sleep, this.CalculateIndividualActivitytreak(logs, ActivityType.Sleep));
            streakInfo.Add(ActivityType.RestingHeartRate, this.CalculateIndividualActivitytreak(logs, ActivityType.RestingHeartRate));

            return streakInfo;
        }

        public int CalculateIndividualActivitytreak(List<ActivityLog> logs, ActivityType activityType)
        {
            DateTime current = DateTime.Today;
            int count = 0;

            while (true)
            {
                bool isExists = logs.Any(log => log.LoggedAt == current);

                if (!isExists)
                {
                    return count;
                }

                HealthStatus status = this.ValidateActivityValue(activityType, this.EvaluateHealthForTheDay(logs, activityType)) == true
                    ? HealthStatus.Healthy
                    : HealthStatus.NeedsAttention;

                if (status != HealthStatus.Healthy)
                {
                    return count;
                }


                count += 1;
                current = current.Date.AddDays(-1);
            }
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

        public decimal EvaluateHealth(List<ActivityLog> logs, ActivityType activityType)
        {
            return this.EvaluateHealthForTheDay(logs, activityType);
        }

        //Steps walked – Total for the day
        public decimal EvaluateHealthForTheDay(List<ActivityLog> logs, ActivityType activityType)
        {
            switch (activityType)
            {
                case ActivityType.StepsWalked:
                    return logs.Sum(log => log.Value);

                case ActivityType.WaterIntake:
                    return logs.Sum(log => log.Value);

                case ActivityType.BodyWeight:
                    return logs.Last().Value;

                case ActivityType.Sleep:
                    return logs.Sum(log => log.Value);

                case ActivityType.RestingHeartRate:
                    return logs.Average(log => log.Value);

                default:
                    return 0;
            }
        }
    }
}
