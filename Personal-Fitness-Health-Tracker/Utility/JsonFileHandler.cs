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

        public static List<T> ReadFromFile(string fileName)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, fileName);
            if (!isFileExists(fileName))
            {
                File.Create(filePath);
            }

            List<T> content = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(filePath)) ?? new List<T>();
            return content;
        }

        public static void WriteToFile(string fileName, List<T> content)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, fileName);
            string jsonContent = JsonSerializer.Serialize(content);

            File.WriteAllText(filePath, jsonContent);
        }
    }
}
