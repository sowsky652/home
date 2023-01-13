using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class SaveLoadSystem
{
    public const int CurrentVersion = 2;

    public static readonly string FileName = "save.json";

    public static string FilePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, FileName);
        }
    }

    public static void Save(SaveData data)
    {
        using (var file = File.CreateText(FilePath))
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(file, data);
        }
    }

    public static SaveData Load()
    {
        SaveData data = null;

        var json = File.ReadAllText(FilePath);
        var jsonObj = JObject.Parse(json);
        var fileVersion = (int)jsonObj["Version"];

        using (var file = File.OpenText(FilePath))
        {
            System.Type t = typeof(SaveData);
            switch (fileVersion)
            {
                case 1:
                    t = typeof(SaveDataV1);
                    break;
            }

            var deserializer = new JsonSerializer();
            data = deserializer.Deserialize(file, t) as SaveData;
        }

        while (data.Version < CurrentVersion)
        {
            data = data.VersionUp();
        }

        return data;
    }
}