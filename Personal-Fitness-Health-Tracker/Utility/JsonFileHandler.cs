using System.Text.Json;

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
            string folderPath = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker");
            string filePath = Path.Combine(folderPath, fileName);

            Directory.CreateDirectory(folderPath);

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
