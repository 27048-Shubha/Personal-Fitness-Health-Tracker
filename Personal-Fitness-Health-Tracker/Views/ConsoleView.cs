using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Views
{
    using Personal_Fitness_Health_Tracker.Enums;
    using Spectre.Console;
    using System.Globalization;

    internal class ConsoleView
    {
        public void DisplayMainMenu()
        {
            AnsiConsole.MarkupLine("[blue]Welcome to Personal Fitness & Health Tracker[/]");
        }

        public void DisplayHealthStatus(HealthStatus status)
        {
            if (status == HealthStatus.Healthy)
            {
                AnsiConsole.MarkupLine($"[green]Activity health status: {status}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]Activity health status: {status}[/]");
            }
        }

        public MainMenu GetMenuOption()
        {
            MainMenu choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenu>()
                    .Title("Select tab")
                    .AddChoices(MainMenu.LogActivityTab, MainMenu.HistoryTab, MainMenu.TrendsTab));

            return choice;
        }

        public string GetActivityName()
        {
            string name = AnsiConsole.Ask<string>("Enter log name: ");
            return name;
        }

        public DateTime GetActivityTime()
        {
            string dateTimeFormat = "dd/MM/yyyy HH:mm";
            string name = AnsiConsole.Ask<string>($"Enter log time: (FORMAT: {dateTimeFormat}) ");
            DateTime parsedInput;

            while (! DateTime.TryParseExact(name, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedInput))
            {
                name = AnsiConsole.Ask<string>($"Invalid format for date time! Valid format: {dateTimeFormat}");
            }

            return parsedInput;
        }

        public ActivityType GetActivity()
        {
            ActivityType choice = AnsiConsole.Prompt(
                    new SelectionPrompt<ActivityType>()
                    .Title("Select activity type")
                    .AddChoices(ActivityType.StepsWalked, ActivityType.Sleep, ActivityType.WaterIntake, ActivityType.BodyWeight, ActivityType.RestingHeartRate));

            return choice;
        }

        public decimal GetActivityValue()
        {
            string name = AnsiConsole.Ask<string>("Enter value recorded: ");
            Decimal parsedInput;

            while (!Decimal.TryParse(name, out parsedInput))
            {
                name = AnsiConsole.Ask<string>($"Invalid value type received! Kindly enter numeric values only");
            }

            return parsedInput;
        }
    }
}
