using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Models.Enums;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierCommandService
{
    public static bool GetAll(out List<SupplierCommand> supplierCommands)
    {
        string sql = "SELECT * FROM `SupplierCommand` INNER JOIN `Supplier` ON `fkSupplier` = `pkSupplier`";

        supplierCommands = new List<SupplierCommand>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            supplierCommands.Add(new SupplierCommand()
            {
                IDSupplierCommand = reader.GetInt32("pkSupplierCommand"),

                CommandDate     = reader.GetDateTime("commandDate"),
                CommandType     = (CommandType)reader.GetInt32("commandType"),
                Status   = (CommandStatus)reader.GetInt32("commandStatus"),

                TotalCost = reader.GetFloat("totalCost"),
                TransportCost = reader.GetFloat("transportCost"),

                Supplier = new Supplier()
                {
                    IDSupplier  = reader.GetInt32("pkSupplier"),

                    Name        = reader.GetString("name"),
                    Email       = reader.GetString("email"),

                    Address     = reader.GetString("address"),
                    Town        = reader.GetString("city"),
                    PostalCode  = reader.GetString("postalCode")
                }
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out SupplierCommand? supplierCommand)
    {
        string sql = $"SELECT * FROM `SupplierCommand` INNER JOIN `Supplier` ON `fkSupplier` = `pkSupplier` WHERE `pkSupplierCommand` = {id}";

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
            IDSupplierCommand = reader.GetInt32("pkSupplierCommand"),

            CommandDate     = reader.GetDateTime("commandDate"),
            CommandType     = (CommandType)reader.GetInt32("commandType"),
            Status   = (CommandStatus)reader.GetInt32("commandStatus"),

            TotalCost       = reader.GetFloat("totalCost"),
            TransportCost   = reader.GetFloat("transportCost"),

            Supplier = new Supplier()
            {
                IDSupplier  = reader.GetInt32("pkSupplier"),

                Name        = reader.GetString("name"),
                Email       = reader.GetString("email"),

                Address     = reader.GetString("address"),
                Town        = reader.GetString("city"),
                PostalCode  = reader.GetString("postalCode")
            }
        };
        reader.Close();


        return true;
    }

    public static bool Add(SupplierCommand supplierCommand)
    {
        string sql = "INSERT INTO `SupplierCommand` (`commandDate`, `totalCost`, `transportCost`, `fkSupplier`) " +
            "VALUES (@commandDate, @totalCost, @transportCost, @fkSupplier)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@commandDate",      supplierCommand.CommandDate);
        command.Parameters.AddWithValue("@totalCost",       supplierCommand.TotalCost);
        command.Parameters.AddWithValue("@transportCost",   supplierCommand.TransportCost);
        command.Parameters.AddWithValue("@fkSupplier",      supplierCommand.Supplier.IDSupplier);

        if (!DBConnection.Execute(command))
            return false;

        supplierCommand.IDSupplierCommand = DBConnection.GetLastPk("SupplierCommand");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("SupplierCommand", id);
    }

    public static bool Update(SupplierCommand supplierCommand)
    {
        string sql = $"UPDATE `SupplierCommand` " +
            $"SET `commandDate` = @commandDate, `totalCost` = @totalCost, `transportCost` = @transportCost, `fkSupplier` = @fkSupplier " +
            $"WHERE `pkSupplierCommand` = {supplierCommand.IDSupplierCommand}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@commandDate",      supplierCommand.CommandDate);
        command.Parameters.AddWithValue("@totalCost",       supplierCommand.TotalCost);
        command.Parameters.AddWithValue("@transportCost",   supplierCommand.TransportCost);
        command.Parameters.AddWithValue("@fkSupplier",      supplierCommand.Supplier.IDSupplier);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
