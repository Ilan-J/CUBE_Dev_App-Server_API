namespace CUBE_Dev_App_Server_API;

public class DBSettings
{
    public string   Host { get; set; }      = "localhost";
    public int      Port { get; set; }      = 3306;

    public string   Database { get; set; }  = string.Empty;

    public string   Username { get; set; }  = string.Empty;
    public string   Password { get; set; }  = string.Empty;

    public override string ToString()
    {
        return $"Host = {Host}; Port = {Port}; Database = {Database}; Username = {Username}; Password = {Password}";
    }
}
