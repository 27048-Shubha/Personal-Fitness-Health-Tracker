using Personal_Fitness_Health_Tracker.Models;
using Personal_Fitness_Health_Tracker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Repositories
{
    internal class ActivityRepository
    {
        private List<ActivityLog> _logs = new();
        private string _fileName = "activityLogs.json";

        public void Add(ActivityLog newLog)
        {
            this._logs.Add(newLog);
            JsonFileHandler<ActivityLog>.WriteToFile(_fileName, _logs);
        }

        public List<ActivityLog> Get()
        {
            this._logs = JsonFileHandler<ActivityLog>.ReadFromFile(_fileName);
            return _logs;
        }

    }
}
