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
            MainMenu type = this._view.GetMenuOption();

            switch (type)
            {
                case MainMenu.HistoryTab:
                    break;

                case MainMenu.LogActivityTab:
                    this.RecordNewActivity();
                    break;

                case MainMenu.TrendsTab:
                    break;
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

            this._view.DisplayHealthStatus(this._activityService.RecordNew(newActivity));
        }

        public void GetTodaysSummary()
        {
            this._activityService.GetTodays();
        }
    }
}
