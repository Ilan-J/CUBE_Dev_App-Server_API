using System.Text.Json;

namespace CUBE_Dev_App_Server_API;

public class DBSettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 3306;

    public string Database { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Host = {Host}; Port = {Port}; Database = {Database}; Username = {Username}; Password = {Password}";
    }

    /// <summary>
    /// Lecture des informations du fichier dbsettings.json pour les mettre dans l'objet
    /// </summary>
    /// <returns></returns>
    public void GetJsonFileData()
    {
        string path = "../dbsettings.json";

        if (!File.Exists(path))
            File.WriteAllText(path, JsonSerializer.Serialize(new DBSettings()));

        StreamReader file = new(path);
        string json = file.ReadToEnd();
        file.Close();

        DBSettings? dbSettings = JsonSerializer.Deserialize<DBSettings>(json);
        if (dbSettings == null)
        {
            dbSettings = new DBSettings();
            Console.WriteLine("Error DBSettings deserialization");
        }
        else
        {
            this.Host = dbSettings.Host;
            this.Port = dbSettings.Port;
            this.Database = dbSettings.Database;
            this.Username = dbSettings.Username;
            this.Password = dbSettings.Password;
        }
    }
}
