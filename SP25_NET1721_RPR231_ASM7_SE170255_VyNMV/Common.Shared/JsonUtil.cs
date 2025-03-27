using System.Text.Json;

namespace Common.Shared;

public static class JsonUtil
{
    public static string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }

    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

    public static void WriteToFile<T>(string filePath, T obj)
    {
        string json = Serialize(obj);
        File.WriteAllText(filePath, json);
    }

    public static T ReadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }
        string json = File.ReadAllText(filePath);
        return Deserialize<T>(json);
    }
}