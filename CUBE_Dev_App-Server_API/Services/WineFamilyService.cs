using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class WineFamilyService
{
    public static bool GetAll(out List<WineFamily>? wineFamilies)
    {
        string sql = @"SELECT * FROM WineFamily ORDER BY idWineFamily";

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
        {
            wineFamilies = null;
            return false;
        }

        wineFamilies = new List<WineFamily>();

        while (reader.Read())
        {
            wineFamilies.Add(new WineFamily
            {
                idWineFamily = reader.GetInt32("idWineFamily"),
                Name = reader.GetString("Name")
            });
        }

        return true;
    }

    public static bool Get(int id, out WineFamily? wineFamily)
    {
        string sql = $"SELECT * FROM WineFamily WHERE idWineFamily = {id}";

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
        {
            wineFamily = null;
            return false;
        }

        wineFamily = new WineFamily();

        if (reader.Read())
        {
            wineFamily = new WineFamily
            {
                idWineFamily = reader.GetInt32("idWineFamily"),
                Name = reader.GetString("Name")
            };
        }
        else
        {
            wineFamily = null;
        }

        return true;
    }

    public static bool Add(WineFamily wineFamily)
    {
        string sql = "INSERT INTO WineFamily" +
            "(Name) " +
            "VALUES (@Name)";

        using MySqlCommand command = new MySqlCommand(sql);

        command.Parameters.AddWithValue("@Name", wineFamily.Name);

        return DBConnection.ExecuteCommandToDB(command);
    }

    public static bool Update(WineFamily wineFamily)
    {
        string sql = @"UPDATE WineFamily SET 
                    Name = @Name
                    WHERE idWineFamily = @idWineFamily";

        using MySqlCommand command = new MySqlCommand(sql);

        command.Parameters.AddWithValue("@idWineFamily", wineFamily.idWineFamily);
        command.Parameters.AddWithValue("@Name", wineFamily.Name);

        return DBConnection.ExecuteCommandToDB(command);
    }

    public static bool Delete(int id)
    {
        string sql = $"DELETE FROM WineFamily WHERE idWineFamily = {id}";

        using MySqlCommand command = new MySqlCommand(sql);

        return DBConnection.ExecuteCommandToDB(command);
    }
}
