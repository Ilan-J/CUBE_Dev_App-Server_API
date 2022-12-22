using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class WineFamilyService
{
    public static bool GetAll(out List<WineFamily> wineFamilies)
    {
        string sql = "SELECT * FROM `WineFamily`";

        wineFamilies = new List<WineFamily>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            wineFamilies.Add(new WineFamily()
            {
                PkWineFamily    = reader.GetInt32("pkWineFamily"),
                Name            = reader.GetString("name")
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out WineFamily? wineFamily)
    {
        string sql = $"SELECT * FROM `WineFamily` WHERE `pkWineFamily` = {id}";
        
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
            PkWineFamily    = reader.GetInt32("pkWineFamily"),
            Name            = reader.GetString("name")
        };
        reader.Close();
        return true;
    }

    public static bool Add(WineFamily wineFamily)
    {
        string sql = "INSERT INTO `WineFamily` (`name`) " +
            "VALUES (@name)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name", wineFamily.Name);

        if (!DBConnection.Execute(command))
            return false;

        wineFamily.PkWineFamily = DBConnection.GetLastPk("WineFamily");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("WineFamily", id);
    }

    public static bool Update(WineFamily wineFamily)
    {
        string sql = $"UPDATE `WineFamily` " +
            $"SET `name` = @name " +
            $"WHERE `pkWineFamily` = {wineFamily.PkWineFamily}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name", wineFamily.Name);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
