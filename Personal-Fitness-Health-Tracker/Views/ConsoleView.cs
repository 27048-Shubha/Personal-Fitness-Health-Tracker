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
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using static System.Runtime.InteropServices.JavaScript.JSType;

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
            AnsiConsole.MarkupLine("[yellow]No Activity Logs Found[/]");
        }
            
        public void DisplayLogs(List<GetActivityInfoDTO> logs)
        {
            var table = new Table();

            table.Title("[blue]Activity Logs[/]");

            table.AddColumn("[yellow]Name[/]"); 
            table.AddColumn("[yellow]Type[/]");
            table.AddColumn("[yellow]LoggedAt[/]");
            table.AddColumn("[yellow]Value[/]");
            table.AddColumn("[yellow]Health status for the day[/]");

            foreach (GetActivityInfoDTO log in logs)
            {
                table.AddRow(log.Name, log.Type.ToString(), log.LoggedAt.ToString(), log.Value.ToString() + this.DisplayMetrics(log.Type), log.Health.ToString());
            }

            AnsiConsole.Write(table);
        }

        public void DisplayPause()
        {
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadLine();
        }

        public void DisplayTodaysSummaryHeader()
        {
            AnsiConsole.MarkupLine("[bold]Today's Activities[/]");
        }
        public void DisplayUpcomingFeature()
        {
            AnsiConsole.MarkupLine("[yellow]Upcoming Feature! Under implementation[/]");
        }

        public void DisplayExportSuccess()
        {
            AnsiConsole.MarkupLine("[green]Export successful! Check your \'Downloads\'![/]");
        }

        public void DisplayStreakInfo(Dictionary<ActivityType, int> streakInfo)
        {
            var maxStreak = streakInfo.Values.Max();
            const int barWidth = 10;

            foreach (var (activity, streak) in streakInfo)
            {
                var filled = maxStreak == 0
                    ? 0
                    : (int)Math.Round((double)streak / maxStreak * barWidth);

                var bar = new string('█', filled) +
                          new string('░', barWidth - filled);

                Console.WriteLine($"{activity}");
                Console.WriteLine($"{bar} {streak}");
            }
        }

        public MainMenu GetMenuOption()
        {
            MainMenu choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenu>()
                    .Title("[yellow]Choose an option[/]")
                    .AddChoices(MainMenu.TodaysSummary, MainMenu.LogActivityTab, MainMenu.HistoryTab, MainMenu.ExportToCSV));

            return choice;
        }

        public ViewMenu GetViewMenuOption()
        {
            ViewMenu choice = AnsiConsole.Prompt(
                    new SelectionPrompt<ViewMenu>()
                    .Title("[yellow]Choose an option[/]")
                    .AddChoices(ViewMenu.All, ViewMenu.Activity, ViewMenu.Date));

            return choice;
        }

        public HistoryTab GetHistoryTabMenu()
        {
            HistoryTab choice = AnsiConsole.Prompt(
                    new SelectionPrompt<HistoryTab>()
                    .Title("[yellow]Choose an option[/]")
                    .AddChoices(HistoryTab.Edit, HistoryTab.Delete, HistoryTab.View, HistoryTab.Search));

            return choice;
        }

        public GetActivityInfoDTO SelectActivity(List<GetActivityInfoDTO> logs)
        {
            GetActivityInfoDTO log = AnsiConsole.Prompt(
                    new SelectionPrompt<GetActivityInfoDTO>()
                    .Title("[yellow]Choose one activity log[/]")
                    .UseConverter(log => $"{log.Name} | {log.LoggedAt:dd-MMM-yyyy} | {log.Type} | {log.Value}")
                    .AddChoices(logs));

            return log;
        }

        public GetActivityInfoDTO GetUpatedLog(GetActivityInfoDTO log)
        {
            log.Name = AnsiConsole.Prompt(
                new TextPrompt<string>($"Name:")
                .DefaultValue(log.Name));

            log.Value = AnsiConsole.Prompt(
                new TextPrompt<decimal>($"Value:")
                .DefaultValue(log.Value));

            log.LoggedAt = AnsiConsole.Prompt(
                new TextPrompt<DateTime>($"Time:")
                .DefaultValue(log.LoggedAt));

            return log;
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

        public string GetString()
        {
            string name = AnsiConsole.Ask<string>("Enter any valid string: ");
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

        public DateTime GetActivityTime()
        {
            string dateTimeFormat = "dd/MM/yyyy HH:mm";
            string name = AnsiConsole.Ask<string>($"Enter date & time to search: ({dateTimeFormat}) ");
            DateTime parsedInput;

            while (!DateTime.TryParseExact(name, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedInput))
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
