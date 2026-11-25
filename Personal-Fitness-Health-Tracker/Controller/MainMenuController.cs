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
            while (true)
            {
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
                        break;

                    case MainMenu.TrendsTab:
                        break;
                }

                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        public void RecordNewActivity()
        {
            ActivityType type = this._view.GetActivity();
            AddActivityDTO newActivity = new AddActivityDTO
            {
                UserId = SessionHandler.CurrentUser.Id,
                Name = this._view.GetActivityName(),
                LoggedAt = this._view.GetActivityTime(),
                Type = type,
                Value = this._view.GetActivityValue(),
            };

            if (! this._activityService.ValidateActivityValue(newActivity.Type, newActivity.Value))
            {
                this._view.DisplayInvalidInputValue(newActivity.Type);
                return;
            }

            this._view.DisplayHealthStatus(this._activityService.RecordNew(newActivity));
        }

        public void GetTodaysSummary()
        {
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
    }
}
