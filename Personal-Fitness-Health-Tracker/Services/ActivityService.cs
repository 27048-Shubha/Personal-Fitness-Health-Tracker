using Personal_Fitness_Health_Tracker.Enums;
using Personal_Fitness_Health_Tracker.Utility;

namespace Personal_Fitness_Health_Tracker.Services
{
    using Personal_Fitness_Health_Tracker.Models;
    using Personal_Fitness_Health_Tracker.Models.DTOs;
    using Personal_Fitness_Health_Tracker.Repositories;

    internal class ActivityService
    {
        private readonly ActivityStreakService _streakService;
        private readonly CSVExportService _csvExportService;
        private readonly ActivityRepository _activityRepository;
        internal ActivityService(ActivityStreakService streakService, CSVExportService csvExportService, ActivityRepository activityRepository)
        {
            this._streakService = streakService;
            this._csvExportService = csvExportService;
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
            if (activityDTO.Type == ActivityType.BMI)
            {
                SessionHandler.CurrentUser.Weight = activityDTO.Value;
                validationValue = (SessionHandler.CurrentUser.Weight ?? activity.Value) / (SessionHandler.CurrentUser.Height * SessionHandler.CurrentUser.Height);
            }
            else
            {
                validationValue = activity.Value;
            }

            return ActivityValidator.ValidateActivityRange(activity.Type, validationValue);
        }

        public List<GetActivityInfoDTO> Get(DateTime dateTime)
        {
            DateTime startOfDay = dateTime;
            DateTime endOfDay = startOfDay.AddDays(1);
            List<GetActivityInfoDTO> logs = this.Get().FindAll(log => (log.LoggedAt >= startOfDay && log.LoggedAt < endOfDay));
            return logs;
        }

        public List<GetActivityInfoDTO> Get()
        {
            return this.MapToDTO(this._activityRepository.Get().FindAll(log => log.UserId == SessionHandler.CurrentUser.Id));
        }
        public List<ActivityLog> GetActivityLog(DateTime dateTime)
        {
            DateTime startOfDay = dateTime;
            DateTime endOfDay = startOfDay.AddDays(1);
            List<ActivityLog> logs = this._activityRepository.Get().FindAll(log => (log.LoggedAt >= startOfDay && log.LoggedAt <= endOfDay));
            return logs;
        }

        public List<GetActivityInfoDTO> GetByActivity(ActivityType type)
        {
            List<GetActivityInfoDTO> logs = this.Get().FindAll(log => log.Type == type);
            return logs;
        }

        public List<GetActivityInfoDTO> Search(string textToSearch)
        {
            List<GetActivityInfoDTO> logs = this.Get();
            List<GetActivityInfoDTO> similarLogs = new();

            foreach (GetActivityInfoDTO log in logs)
            {
                if ((log.Name.Contains(textToSearch, StringComparison.OrdinalIgnoreCase))
                    || (log.LoggedAt.ToString().Contains(textToSearch))
                    || (log.Type.ToString().Contains(textToSearch))
                    || (log.Value.ToString().Contains(textToSearch)))
                {
                    similarLogs.Add(log);
                }
            }

            return similarLogs;
        }

        public List<GetActivityInfoDTO> MapToDTO(List<ActivityLog> logs)
        {
            if (logs == null)
            {
                return null;
            }

            List<GetActivityInfoDTO> activity = new();
            foreach (ActivityLog log in logs)
            {
                var dayLogs = logs
                    .Where(x => x.LoggedAt.Date == log.LoggedAt.Date)
                    .ToList();


                activity.Add(
                    new GetActivityInfoDTO
                    {
                        Id = log.Id,
                        Name = log.Name,
                        LoggedAt = log.LoggedAt,
                        Type = log.Type,
                        Value = log.Value,
                        Health = this._streakService.EvaluateHealthForTheDay(dayLogs, log.Type),
                    }
                );
            }

            return activity;
        }

        public bool ValidateActivityValue(ActivityType activityType, decimal activityValue)
        {
            return this._streakService.ValidateActivityValue(activityType, activityValue);
        }

        public bool ValidateDate(DateTime date)
        {
            return date <= DateTime.Now;
        }

        public Dictionary<ActivityType, int> GetStreakInfo()
        {
            return this._streakService.GetStreakInfo(this._activityRepository.Get());
        }

        public void ExportToCSV()
        {
            this._csvExportService.ExportToCSV(this.Get());
        }

        public void Delete(GetActivityInfoDTO log)
        {
            this._activityRepository.Delete(log.Id);
        }

        public bool Update(GetActivityInfoDTO log)
        {
            ActivityLog? logToBeUpdated = this._activityRepository.Find(log.Id);

            if (logToBeUpdated == null)
            {
                return false;
            }

            if (!this.ValidateDate(log.LoggedAt))
            {
                return false;
            }

            if (!this.ValidateActivityValue(log.Type, log.Value))
            {
                return false;
            }

            logToBeUpdated.Name = log.Name;
            logToBeUpdated.Type = log.Type;
            logToBeUpdated.LoggedAt = log.LoggedAt;
            logToBeUpdated.Value = log.Value;

            this._activityRepository.Update(logToBeUpdated, log.Id);

            return true;
        }
    }
}
