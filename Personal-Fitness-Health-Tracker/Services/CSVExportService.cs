using Personal_Fitness_Health_Tracker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Personal_Fitness_Health_Tracker.Services
{
    internal class CSVExportService
    {
        public void ExportToCSV(List<GetActivityInfoDTO> data)
        {
            string convertedData = this.ConvertToCSV(data);
            this.DownloadCSV(convertedData);
        }

        public string ConvertToCSV(List<GetActivityInfoDTO> data)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Name, Type, Value, LoggedAt");

            foreach(GetActivityInfoDTO item in data)
            {
                string row = string.Join(" ", item.Name,
                    item.Type.ToString(),
                    item.Value.ToString(),
                    item.LoggedAt.ToString());

                csvData.AppendLine(row);
            }

            return csvData.ToString();
        }

        public void DownloadCSV(string csvData)
        {
            string downloadsFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads");

            string filePath = Path.Combine(
                downloadsFolder,
                "ActivityReport.csv");

            File.WriteAllText(filePath, csvData.ToString());
        }
    }
}
