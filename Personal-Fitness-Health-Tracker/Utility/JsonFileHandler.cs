using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Personal_Fitness_Health_Tracker.Utility
{
    internal static class JsonFileHandler<T>
    {
        private static bool isFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string GetFilePath(string fileName)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, fileName);
            if (!isFileExists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
            return filePath;
        }

        public static List<T> ReadFromFile(string fileName)
        {
            string filePath = GetFilePath(fileName);

            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<T>();
            }

            return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(filePath)) ?? new List<T>();
        }

        public static void WriteToFile(string fileName, List<T> content)
        {
            string filePath = GetFilePath(fileName);
            File.WriteAllText(filePath, JsonSerializer.Serialize(content));
        }
    }
}
