using Personal_Fitness_Health_Tracker.Controller;
using Personal_Fitness_Health_Tracker.Repositories;
using Personal_Fitness_Health_Tracker.Services;
using Personal_Fitness_Health_Tracker.Views;

namespace Personal_Fitness_Health_Tracker
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ConsoleView console = new ConsoleView();
            ActivityRepository activityRepository = new ActivityRepository();
            ActivityStreakService streakService = new ActivityStreakService();
            CSVExportService exportService = new();
            ActivityService activityService = new ActivityService(streakService, exportService, activityRepository);
            MainMenuController mainMenuController = new MainMenuController(activityService, console);

            mainMenuController.RunMainMenu();
        }
    }
}
