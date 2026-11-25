using Personal_Fitness_Health_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Utility
{
    using static Personal_Fitness_Health_Tracker.Utility.SessionHandler;
    internal static class ActivityValidator
    {
        public static HealthStatus ValidateActivityRange(ActivityType activtiyType, decimal value)
        {
            (decimal minRange, decimal maxRange) = GetMinMaxRange(activtiyType);
            if (value < minRange || value  > maxRange)
            {
                return HealthStatus.NeedsAttention;
            }

            return HealthStatus.Healthy;
        }

        public static (decimal MinRange, decimal MaxRange) GetMinMaxRange (ActivityType activtiyType)
        {
            switch (activtiyType)
            {
                case ActivityType.StepsWalked:
                    return (7000, 10000);

                case ActivityType.Sleep:
                    return (7, 9);

                case ActivityType.WaterIntake:
                    return (3, 5);

                case ActivityType.BodyWeight:
                    return (18, 25);

                case ActivityType.RestingHeartRate:
                    return (60, 100);

                default:
                    return (0, 0);
            }
        }
    }
}
