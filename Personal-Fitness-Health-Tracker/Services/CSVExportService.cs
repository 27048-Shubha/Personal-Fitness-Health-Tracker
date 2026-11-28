using Personal_Fitness_Health_Tracker.Models.DTOs;
using System.Text;

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
                string row = string.Join(",", item.Name,
                    item.Type.ToString(),
                    item.Value.ToString(),
                    item.LoggedAt.ToString());

                csvData.AppendLine(row);
            }

            return csvData.ToString();
        }

        public void DownloadCSV(string csvData)
        {
            string folderPath = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker");
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine( folderPath, $"ActivityReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

            File.WriteAllText(filePath, csvData);
        }
    }
}
