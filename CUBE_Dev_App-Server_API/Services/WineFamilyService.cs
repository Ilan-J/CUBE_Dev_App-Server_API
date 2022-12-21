using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class WineFamilyService
{
    public static bool GetAll(out List<WineFamily> wineFamilies)
    {
        string sql = "SELECT * FROM `wine_family`";

        wineFamilies = new List<WineFamily>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            wineFamilies.Add(new WineFamily()
            {
                PkWineFamily    = reader.GetInt32("pk_wine_family"),
                Name            = reader.GetString("name")
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out WineFamily? wineFamily)
    {
        string sql = $"SELECT * FROM `wine_family` " +
            $"WHERE `pk_wine_family` = {id}";
        
        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
        {
            wineFamily = null;
            return false;
        }

        if (!reader.Read())
        {
            wineFamily = null;
            return true;
        }

        wineFamily = new WineFamily()
        {
            PkWineFamily    = reader.GetInt32("pk_wine_family"),
            Name            = reader.GetString("name")
        };
        reader.Close();
        return true;
    }

    public static bool Add(WineFamily wineFamily)
    {
        string sql = "INSERT INTO `wine_family` (`name`) " +
            "VALUES (@name)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name", wineFamily.Name);

        if (!DBConnection.Execute(command))
            return false;

        wineFamily.PkWineFamily = DBConnection.GetLastPk("wine_family");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("wine_family", id);
    }

    public static bool Update(WineFamily wineFamily)
    {
        string sql = $"UPDATE `wine_family` " +
            $"SET `name` = @name " +
            $"WHERE `pk_wine_family` = {wineFamily.PkWineFamily}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name", wineFamily.Name);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
