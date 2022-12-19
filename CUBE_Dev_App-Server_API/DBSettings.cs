namespace CUBE_Dev_App_Server_API;

public class DBSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }

    public string? Database { get; set; }

    public string? Username { get; set; }
    public string? Password { get; set; }

    public override string ToString()
    {
        return $"Host = {Host}; Port = {Port}; Database = {Database}; Username = {Username}; Password = {Password}";
    }
}
