using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API;

public static class DBConnection
{
    public static MySqlConnection Connection { get; }
    static DBConnection()
    {
        DBSettings? dbSettings = Settings.GetDBSettings();

        if (dbSettings is null)
            throw new ArgumentNullException(nameof(dbSettings));

        Connection = new MySqlConnection(dbSettings.ToString());
    }

    public static void Uptade(DBSettings dbSettings)
    {

    }
}
