using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierCommandService
{
    public static bool GetAll(out List<object> supplierCommands)
    {
        string sql = "SELECT * FROM `SupplierCommand`";

        supplierCommands = new List<object>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            supplierCommands.Add(new object()
            {
                
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out object? supplierCommand)
    {
        string sql = $"SELECT * FROM `SupplierCommand` WHERE `pkSupplierCommand` = {id}";

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
        {
            supplierCommand = null;
            return false;
        }

        if (!reader.Read())
        {
            supplierCommand = null;
            return true;
        }

        supplierCommand = new object()
        {

        };
        reader.Close();
        return true;
    }

    public static bool Add(object supplierCommand)
    {
        string sql = "INSERT INTO `SupplierCommand` (``) " +
            "VALUES ()";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name", supplierCommand);

        if (!DBConnection.Execute(command))
            return false;

        supplierCommand = DBConnection.GetLastPk("WineFamily");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("SupplierCommand", id);
    }

    public static bool Update(object supplierCommand)
    {
        string sql = $"UPDATE `SupplierCommand` " +
            $"SET `` =  " +
            $"WHERE `pkSupplierCommand` = {supplierCommand}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("", supplierCommand);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
