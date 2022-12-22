using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierCommandService
{
    public static bool GetAll(out List<SupplierCommand> supplierCommands)
    {
        string sql = "SELECT * FROM `SupplierCommand`";

        supplierCommands = new List<SupplierCommand>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            supplierCommands.Add(new SupplierCommand()
            {
                
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out SupplierCommand? supplierCommand)
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

        supplierCommand = new SupplierCommand()
        {

        };
        reader.Close();
        return true;
    }

    public static bool Add(SupplierCommand supplierCommand)
    {
        string sql = "INSERT INTO `SupplierCommand` (``) " +
            "VALUES (@)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@", supplierCommand);

        if (!DBConnection.Execute(command))
            return false;

        supplierCommand.PkSupplierCommand = DBConnection.GetLastPk("SupplierCommand");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("SupplierCommand", id);
    }

    public static bool Update(SupplierCommand supplierCommand)
    {
        string sql = $"UPDATE `SupplierCommand` " +
            $"SET `` = @ " +
            $"WHERE `pkSupplierCommand` = {supplierCommand.PkSupplierCommand}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@", supplierCommand);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
