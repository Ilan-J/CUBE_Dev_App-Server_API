using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API;

public static class DBConnection
{
    public static bool GetConnection(out MySqlConnection connection)
    {
        connection = new(Settings.GetDBSettings());
        try
        {
            connection.Open();
        }
        catch (MySqlException exception)
        {
            Console.WriteLine(exception.Message);
            return false;
        }
        return true;
    }
}
