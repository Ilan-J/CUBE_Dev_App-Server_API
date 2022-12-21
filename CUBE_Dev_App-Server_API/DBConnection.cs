using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API;

public static class DBConnection
{
    public static MySqlConnection Connection { get; }

    static DBConnection()
    {
        DBSettings dbSettings = Settings.GetDBSettings();

        Connection = new MySqlConnection(dbSettings.ToString());

        if (!Open()) Console.Error.WriteLine("Failed Initial DB Connection!");
    }

    /*public static void UptadeSettings(DBSettings dbSettings)
    {
    }*/

    /// <summary>
    /// Opens connection to database
    /// </summary>
    /// <returns>true if success, otherwise false</returns>
    public static bool Open()
    {
        try
        {
            Connection.Open();
        }
        catch(MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Closes connection to database
    /// </summary>
    public static void Close()
    {
        Connection.Close();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>MySqlDataReader</returns>
    public static MySqlDataReader? ExecuteReader(string sql)
    {
        try
        {
            return new MySqlCommand(sql, Connection).ExecuteReader();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public static bool Execute(string sql)
    {
        try
        {
            new MySqlCommand(sql, Connection).ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public static bool Execute(MySqlCommand mySqlCommand)
    {
        mySqlCommand.Connection = Connection;
        try
        {
            mySqlCommand.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public static bool Delete(string table, int id)
    {
        string sql = $"DELETE FROM `{table}` WHERE `pk_{table}` = {id}";

        return Execute(sql);
    }
    
    public static int GetLastPk(string table)
    {
        string sql = $"SELECT MAX(`pk_{table}`) FROM `{table}`";

        MySqlDataReader? reader = ExecuteReader(sql);
        if (reader is null || !reader.Read())
            return 0;

        int pk = reader.GetInt32(0);

        reader.Close();
        return pk;
    }
}
