using UnityEngine;
using System.IO;

public static class WorldSaveManager
{
    static string path => Application.persistentDataPath + "/world.json";

    public static bool WorldExists()
    {
        return File.Exists(path);
    }

    public static void SaveWorld(int seed)
    {
        WorldData data = new WorldData { seed = seed };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static int LoadWorldSeed()
    {
        if (!WorldExists()) return 0;

        string json = File.ReadAllText(path);
        WorldData data = JsonUtility.FromJson<WorldData>(json);
        return data.seed;
    }
}
