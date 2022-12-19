using System.Text.Json;

namespace CUBE_Dev_App_Server_API;

public static class Settings
{
    public static DBSettings? GetDBSettings()
    {
        string path = "dbsettings.json";

        if (!File.Exists(path))
            File.WriteAllText(path, JsonSerializer.Serialize(new DBSettings()));

        StreamReader file = new(path);
        string json = file.ReadToEnd();
        file.Close();
        
        return JsonSerializer.Deserialize<DBSettings>(json);
    }
}
