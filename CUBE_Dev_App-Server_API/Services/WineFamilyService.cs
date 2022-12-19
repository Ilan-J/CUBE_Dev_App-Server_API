using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class WineFamilyService
{
    public static List<WineFamily> GetAll()
    {
        string sql = "SELECT * FROM `wine_family`";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        List<WineFamily> wineFamilies = new();
        while (reader.Read())
        {
            wineFamilies.Add(new WineFamily()
            {
                PkWineFamily =  reader.GetInt32("pk_wine_family"),
                Name =          reader.GetString("name")
            });
        }
        DBConnection.Connection.Close();

        return wineFamilies;
    }

    public static WineFamily? Get(int id)
    {
        string sql = $"SELECT * FROM `wine_family` " +
            $"WHERE `pk_wine_family` = {id}";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        if (!reader.Read())
            return null;

        WineFamily wineFamily = new()
        {
            PkWineFamily =  reader.GetInt32("pk_wine_family"),
            Name =          reader.GetString("name")
        };
        DBConnection.Connection.Close();

        return wineFamily;
    }

    public static void Add(WineFamily wineFamily)
    {
        string sql = "INSERT INTO `wine_family` (`name`) " +
            "VALUES (@name)";

        MySqlCommand command = new(sql, DBConnection.Connection);

        command.Parameters.AddWithValue("@name", wineFamily.Name);

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM `wine_family` " +
            $"WHERE `pk_wine_family` = {id}";

        DBConnection.Connection.Open();
        new MySqlCommand(sql, DBConnection.Connection).ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Update(WineFamily wineFamily)
    {
        string sql = $"UPDATE `wine_family` " +
            $"SET `name` = @name " +
            $"WHERE `pk_wine_family` = {wineFamily.PkWineFamily}`";

        MySqlCommand command = new(sql, DBConnection.Connection);

        command.Parameters.AddWithValue("@name", wineFamily.Name);

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }
}
