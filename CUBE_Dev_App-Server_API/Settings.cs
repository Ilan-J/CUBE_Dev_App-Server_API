namespace CUBE_Dev_App_Server_API;

public static class Settings
{
    public static string GetDBSettings()
    {
        string host = "localhost";
        int port = 3306;

        string database = "db_negosud";
        string uid = "root";
        string pwd = "1234";

        return $"server={host}; port={port}; database={database}; uid={uid}; pwd={pwd}";
    }
}
