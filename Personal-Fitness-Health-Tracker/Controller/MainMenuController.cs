using Personal_Fitness_Health_Tracker.Models.DTOs;
using Personal_Fitness_Health_Tracker.Services;
using Personal_Fitness_Health_Tracker.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Controller
{
    using Personal_Fitness_Health_Tracker.Enums;
    using Personal_Fitness_Health_Tracker.Views;

    internal class MainMenuController
    {
        private ActivityService _activityService;
        private ConsoleView _view;
        internal MainMenuController(ActivityService activityService, ConsoleView console)
        {
            this._activityService = activityService;
            this._view = console;
        }

        public void RunMainMenu()
        {
            this._view.DisplayHeader();
            while (true)
            {
                this.ShowStreak();

                MainMenu type = this._view.GetMenuOption();
                switch (type)
                {
                    case MainMenu.TodaysSummary:
                        this.GetTodaysSummary();
                        break;

                    case MainMenu.LogActivityTab:
                        this.RecordNewActivity();
                        break;

                    case MainMenu.HistoryTab:
                        this.RunHistoryTabMenu();
                        break;

                    case MainMenu.ExportToCSV:
                        this.ExportAsCSV();
                        break;
                }

                this._view.DisplayPause();
                Console.Clear();
            }
        }

        public void RecordNewActivity()
        {
            ActivityType type = this._view.GetActivity();

            this._view.DisplayActivityRule(type);
            AddActivityDTO newActivity = new AddActivityDTO
            {
                UserId = SessionHandler.CurrentUser.Id,
                Name = this._view.GetActivityName(type),
                LoggedAt = this._view.GetActivityTime(type),
                Type = type,
                Value = this._view.GetActivityValue(type),
            };

            if (! this._activityService.ValidateActivityValue(newActivity.Type, newActivity.Value))
            {
                this._view.DisplayInvalidInputValue(newActivity.Type);
                return;
            }

            this._view.DisplayHealthStatus(this._activityService.RecordNew(newActivity));
        }

        public void ShowStreak()
        {
            Dictionary<ActivityType, int> streakInfo = this._activityService.GetStreakInfo();
            this._view.DisplayStreakInfo(streakInfo);
        }

        public void GetTodaysSummary()
        {
            this._view.DisplayTodaysSummaryHeader();
            List<GetActivityInfoDTO> logs =  this._activityService.Get(DateTime.Today);
            if (logs == null || logs.Count == 0)
            {
                this._view.DisplayEmpty();
            }
            else
            {
                this._view.DisplayLogs(logs);
            }
        }

        public void RunViewMenu()
        {
            ViewMenu type = this._view.GetViewMenuOption();
            switch (type)
            {
                case ViewMenu.All:
                    this.DisplayAll();
                    return;

                case ViewMenu.Activity:
                    this.FilterByActivity();
                    return;

                case ViewMenu.Date:
                    this.FilterByDate();
                    return;
            }

            this._view.DisplayPause();
            Console.Clear();
        }

        public void RunHistoryTabMenu()
        {
            HistoryTab type = this._view.GetHistoryTabMenu();
            switch (type)
            {
                case HistoryTab.Edit:
                    this.UpdateActivity();
                    return;

                case HistoryTab.Delete:
                    this.DeleteActivity();
                    return;

                case HistoryTab.View:
                    this.RunViewMenu();
                    return;

                case HistoryTab.Search:
                    this.SearchActivity();
                    return;
            }

            this._view.DisplayPause();
            Console.Clear();
        }

        public void DisplayAll()
        {
            List<GetActivityInfoDTO> logs = this._activityService.Get();

            if (logs == null || logs.Count == 0)
            {
                this._view.DisplayEmpty();
                return;
            }

            this._view.DisplayLogs(logs);
        }

        public void FilterByActivity()
        {
            ActivityType type = this._view.GetActivity();
            List<GetActivityInfoDTO> logs = this._activityService.GetByActivity(type);
            if (logs == null || logs.Count == 0)
            {
                this._view.DisplayEmpty();
                return;
            }

            this._view.DisplayLogs(logs);
        }

        public void FilterByDate()
        {
            DateTime dateTime = this._view.GetActivityTime();
            List<GetActivityInfoDTO> logs = this._activityService.Get(dateTime);
            if (logs == null || logs.Count == 0)
            {
                this._view.DisplayEmpty();
                return;
            }

            this._view.DisplayLogs(logs);
        }

        public void SearchActivity()
        {
            string textToSearch = this._view.GetString();
            List<GetActivityInfoDTO> logs = this._activityService.Search(textToSearch);

            if (logs == null || logs.Count == 0)
            {
                this._view.DisplayEmpty();
                return;
            }

            this._view.DisplayLogs(logs);
        }

        public void DeleteActivity()
        {
            List<GetActivityInfoDTO> logs = this._activityService.Get();
            if (logs == null || logs.Count == 0)
            {
                this._view.DisplayEmpty();
                return;
            }

            GetActivityInfoDTO log = this._view.SelectActivity(logs);
            this._activityService.Delete(log);
        }

        public void UpdateActivity()
        {
            List<GetActivityInfoDTO> logs = this._activityService.Get();
            if (logs == null || logs.Count == 0)
            {
                this._view.DisplayEmpty();
                return;
            }

            GetActivityInfoDTO log = this._view.SelectActivity(logs);
            log = this._view.GetUpatedLog(log);
            this._activityService.Update(log);
        }

        public void ExportAsCSV()
        {
            this._activityService.ExportToCSV();
            this._view.DisplayExportSuccess();
        }
    }
}
