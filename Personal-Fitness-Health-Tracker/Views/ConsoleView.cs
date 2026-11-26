using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Views
{
    using Personal_Fitness_Health_Tracker.Enums;
    using Personal_Fitness_Health_Tracker.Models.DTOs;
    using Spectre.Console;
    using System.Globalization;

    internal class ConsoleView
    {
        private string rulesForStepsWalked = "Value must be greater than zero";
        private string rulesForHoursSlept = "Valid Range (0 to 24)[/]";
        private string rulesForWaterIntake = "Valid Range (0 to 10)";
        private string rulesForWeight = "Valid Range (20 to 250)";
        private string rulesForHeartRate = "Valid Range (20 to 150)";

        public void DisplayHeader()
        {
            AnsiConsole.MarkupLine("[blue] Personal Fitness & Health Tracker[/]\n");
            AnsiConsole.MarkupLine("[blue] -------------Welcome-------------[/]\n");
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

        public void DisplayEmpty()
        {
            AnsiConsole.MarkupLine(":warning: [yellow]No Activity Logs Found[/]");
        }
            
        public void DisplayLogs(List<GetActivityInfoDTO> logs)
        {
            var table = new Table();

            table.Title("[blue]Activity Logs[/]");

            table.AddColumn("[yellow]Name[/]"); 
            table.AddColumn("[yellow]Type[/]");
            table.AddColumn("[yellow]LoggedAt[/]");
            table.AddColumn("[yellow]Value[/]");

            foreach (GetActivityInfoDTO log in logs)
            {
                table.AddRow(log.Name, log.Type.ToString(), log.LoggedAt.ToString(), log.Value.ToString() + this.DisplayMetrics(log.Type));
            }

            AnsiConsole.Write(table);
        }

        public void DisplayPause()
        {
            Console.WriteLine("Returning Back to Main Menu...");
        }

        public void DisplayTodaysSummaryHeader()
        {
            AnsiConsole.MarkupLine("[bold]Today's Activities[/]");
        }
        public void DisplayUpcomingFeature()
        {
            AnsiConsole.MarkupLine("[yellow]Upcoming Feature! Under implementation[/]");
        }

        public MainMenu GetMenuOption()
        {
            MainMenu choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenu>()
                    .Title("[yellow]Choose an option[/]")
                    .AddChoices(MainMenu.TodaysSummary, MainMenu.LogActivityTab, MainMenu.HistoryTab, MainMenu.TrendsTab));

            return choice;
        }

        public void DisplayActivityRule(ActivityType type)
        {
            switch (type)
            {
                case ActivityType.StepsWalked:
                    AnsiConsole.MarkupLine($"RULE: [yellow]{rulesForStepsWalked}[/]");
                    break;

                case ActivityType.Sleep:
                    AnsiConsole.MarkupLine($"RULE: [yellow]{rulesForHoursSlept}[/]");
                    break;

                case ActivityType.WaterIntake:
                    AnsiConsole.MarkupLine($"RULE: [yellow]{rulesForWaterIntake}[/]");
                    break;

                case ActivityType.BodyWeight:
                    AnsiConsole.MarkupLine($"RULE: [yellow]{rulesForWeight}[/]");
                    break;

                case ActivityType.RestingHeartRate:
                    AnsiConsole.MarkupLine($"RULE: [yellow]{rulesForHeartRate}[/]");
                    break;
            }
        }

        public string GetActivityName(ActivityType type)
        {
            string name = AnsiConsole.Ask<string>($"({type.ToString()}) Enter log name: ");
            return name;
        }

        public DateTime GetActivityTime(ActivityType type)
        {
            string dateTimeFormat = "dd/MM/yyyy HH:mm";
            string name = AnsiConsole.Ask<string>($"({type.ToString()}) Enter log time: ({dateTimeFormat}) ");
            DateTime parsedInput;

            while (! DateTime.TryParseExact(name, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedInput))
            {
                name = AnsiConsole.Ask<string>($"Invalid format for date time! Valid format: {dateTimeFormat}");
            }

            return parsedInput;
        }

        public decimal GetActivityValue(ActivityType type)
        {
            string name = AnsiConsole.Ask<string>($"({type.ToString()}) Enter value recorded: ");
            Decimal parsedInput;

            while (!Decimal.TryParse(name, out parsedInput))
            {
                name = AnsiConsole.Ask<string>($"Invalid value type received! Kindly enter numeric values only");
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

        public void DisplayInvalidInputValue(ActivityType type)
        {
            switch (type)
            {
                case ActivityType.StepsWalked:
                    AnsiConsole.MarkupLine("[yellow]Invalid Input: Value must be greater than zero[/]");
                    break;

                case ActivityType.Sleep:
                    AnsiConsole.MarkupLine("[yellow]Invalid Input: Valid Range (0 to 24)[/]");
                    break;

                case ActivityType.WaterIntake:
                    AnsiConsole.MarkupLine("[yellow]Invalid Input: Valid Range (0 to 10)[/]");
                    break;

                case ActivityType.BodyWeight:
                    AnsiConsole.MarkupLine("[yellow]Invalid Input: Valid Range (20 to 250)[/]");
                    break;

                case ActivityType.RestingHeartRate:
                    AnsiConsole.MarkupLine("[yellow]Invalid Input: Valid Range (20 to 150)[/]");
                    break;
            }
        }

        public string DisplayMetrics(ActivityType type)
        {
            switch (type)
            {
                case ActivityType.StepsWalked:
                    return "steps";

                case ActivityType.Sleep:
                    return "hours";

                case ActivityType.WaterIntake:
                    return "litres";

                case ActivityType.BodyWeight:
                    return "kg";

                case ActivityType.RestingHeartRate:
                    return "bpm";

                default:
                    return string.Empty;
            }
        }
    }
}
