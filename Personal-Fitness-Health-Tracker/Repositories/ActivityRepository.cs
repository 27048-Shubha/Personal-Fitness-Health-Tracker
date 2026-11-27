using Personal_Fitness_Health_Tracker.Models;
using Personal_Fitness_Health_Tracker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Repositories
{
    internal class ActivityRepository
    {
        private List<ActivityLog> _logs = new();
        private string _fileName = "activityLogs.json";

        public void Write()
        {
            JsonFileHandler<ActivityLog>.WriteToFile(_fileName, _logs);
        }

        public void Add(ActivityLog newLog)
        {
            this._logs.Add(newLog);
            this.Write();
        }

        public List<ActivityLog> Get()
        {
            this._logs = JsonFileHandler<ActivityLog>.ReadFromFile(_fileName);
            return _logs;
        }

        public ActivityLog? Find(Guid id)
        {
            return this._logs.Find(log => log.Id == id);
        }

        public int FindIndex(Guid id)
        {
            return this._logs.FindIndex(log => log.Id == id);
        }

        public void Delete(Guid id)
        {
            ActivityLog? log = this.Find(id);
            if (log != null)
            {
                this._logs.Remove(log);
                this.Write();
            }
        }

        public void Update(ActivityLog updatedLog, Guid id)
        {
            int index = this.FindIndex(id);
            if (index != -1)
            {
                this._logs[index] = updatedLog; 
                this.Write();
            }
        }
    }
}
