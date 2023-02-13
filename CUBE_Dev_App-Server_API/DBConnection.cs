using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API;

public static class DBConnection
{
    public static MySqlConnection Connection { get; }

    static DBConnection()
    {
        DBSettings dbSettings = new DBSettings();
        dbSettings.GetJsonFileData();

        Connection = new MySqlConnection(dbSettings.ToString());

        if (!Open())
            Console.Error.WriteLine("Failed Initial DB Connection !");
    }

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
    /// Execute the "Select" query in the database
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>The query result as a MySqlDataReader</returns>
    public static MySqlDataReader? ExecuteReader(string sql)
    {
        if (!Connection.Ping() && !Open())
            return null;

        try
        {
            MySqlDataReader? mySqlDataReader = new MySqlCommand(sql, Connection).ExecuteReader();
            return mySqlDataReader;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Execute the SQL command in the database
    /// </summary>
    /// <param name="mySqlCommand"></param>
    /// <returns>True if the command was successful, otherwise false.</returns>
    public static bool ExecuteCommandToDB(MySqlCommand mySqlCommand)
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

    #region Global Requests

    /// <summary>
    /// Gets the highest primary key of a table
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static int GetHighestPrimaryKey(string tableName)
    {
        return GetHighestPrimaryKey(tableName, "ID" + tableName);
    }

    /// <summary>
    /// Gets the highest primary key of a tableName
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="idField"></param>
    /// <returns></returns>
    public static int GetHighestPrimaryKey(string tableName, string idField)
    {
        string sql = $"SELECT MAX({idField}) FROM {tableName}";

        using MySqlDataReader? reader = ExecuteReader(sql);
        if (reader == null || !reader.Read())
            return -1;

        int primaryKey = reader.GetInt32(0);

        return primaryKey;
    } 

    #endregion
}
