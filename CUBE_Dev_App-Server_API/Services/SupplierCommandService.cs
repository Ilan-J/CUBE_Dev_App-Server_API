using CUBE_Dev_App_Server_API.Models;
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
                PkSupplierCommand = reader.GetInt32("pkSupplierCommand"),

                CommandDate     = reader.GetDateTime("commandDate"),
                CommandType     = (CommandType)reader.GetInt32("commandType"),
                CommandStatus   = (CommandStatus)reader.GetInt32("commandStatus"),

                TotalCost = reader.GetFloat("totalCost"),
                TransportCost = reader.GetFloat("transportCost"),

                Supplier = new Supplier()
                {
                    PkSupplier  = reader.GetInt32("pkSupplier"),

                    Name        = reader.GetString("name"),
                    Email       = reader.GetString("email"),

                    Address     = reader.GetString("address"),
                    City        = reader.GetString("city"),
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
            PkSupplierCommand = reader.GetInt32("pkSupplierCommand"),

            CommandDate     = reader.GetDateTime("commandDate"),
            CommandType     = (CommandType)reader.GetInt32("commandType"),
            CommandStatus   = (CommandStatus)reader.GetInt32("commandStatus"),

            TotalCost       = reader.GetFloat("totalCost"),
            TransportCost   = reader.GetFloat("transportCost"),

            Supplier = new Supplier()
            {
                PkSupplier  = reader.GetInt32("pkSupplier"),

                Name        = reader.GetString("name"),
                Email       = reader.GetString("email"),

                Address     = reader.GetString("address"),
                City        = reader.GetString("city"),
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
        command.Parameters.AddWithValue("@fkSupplier",      supplierCommand.Supplier.PkSupplier);

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
            $"SET `commandDate` = @commandDate, `totalCost` = @totalCost, `transportCost` = @transportCost, `fkSupplier` = @fkSupplier " +
            $"WHERE `pkSupplierCommand` = {supplierCommand.PkSupplierCommand}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@commandDate",      supplierCommand.CommandDate);
        command.Parameters.AddWithValue("@totalCost",       supplierCommand.TotalCost);
        command.Parameters.AddWithValue("@transportCost",   supplierCommand.TransportCost);
        command.Parameters.AddWithValue("@fkSupplier",      supplierCommand.Supplier.PkSupplier);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
