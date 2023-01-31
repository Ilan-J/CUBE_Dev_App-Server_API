using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ClientCommandService
{
    public static bool GetAll(out List<ClientCommand> clientCommands)
    {
        string sql = @"SELECT * FROM `ClientCommand`";

        clientCommands = new List<ClientCommand>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            clientCommands.Add(new ClientCommand()
            {
                IDClientCommand = reader.GetInt32("pkClientCommand"),

                CommandDate = reader.GetDateTime("commandDate"),
                Status = (CommandStatus)reader.GetInt32("commandStatus"),

                Address = reader.GetString("address"),
                City = reader.GetString("city"),
                Region = reader.GetString("region"),
                PostalCode = reader.GetString("postalCode"),
                Country = reader.GetString("country"),

                TotalCost = reader.GetFloat("totalCost"),
                TransportCost = reader.GetFloat("transportCost"),

                Client = new Client()
                {
                    IDClient = reader.GetInt32("fkClient")
                }
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out ClientCommand? clientCommand)
    {
        string sql = $@"SELECT * FROM `ClientCommand` 
            WHERE `pkClientCommand` = {id}";

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
        {
            clientCommand = null;
            return false;
        }
        if (!reader.Read())
        {
            reader.Close();
            clientCommand = null;
            return true;
        }

        clientCommand = new ClientCommand()
        {
            IDClientCommand = reader.GetInt32("pkClientCommand"),

            CommandDate = reader.GetDateTime("commandDate"),
            Status = (CommandStatus)reader.GetInt32("commandStatus"),

            Address = reader.GetString("address"),
            City = reader.GetString("city"),
            Region = reader.GetString("region"),
            PostalCode = reader.GetString("postalCode"),
            Country = reader.GetString("country"),

            TotalCost = reader.GetFloat("totalCost"),
            TransportCost = reader.GetFloat("transportCost"),

            Client = new Client()
            {
                IDClient = reader.GetInt32("fkClient")
            }
        };
        reader.Close();
        return true;
    }

    public static bool Add(ClientCommand clientCommand)
    {
        string sql = @"INSERT INTO `ClientCommand` (`commandDate`, `commandStatus`, `address`, `city`, `region`, `postalCode`, `country`, `totalCost`, `transportCost`, `fkClient`) 
            VALUES @commandDate, @commandStatus, @address, @city, @region, @postalCode, @country, @totalCost, @transportCost, @fkClient";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@commandDate", clientCommand.CommandDate);
        command.Parameters.AddWithValue("@commandStatus", clientCommand.Status);

        command.Parameters.AddWithValue("@address", clientCommand.Address);
        command.Parameters.AddWithValue("@city", clientCommand.City);
        command.Parameters.AddWithValue("@region", clientCommand.Region);
        command.Parameters.AddWithValue("@postalCode", clientCommand.PostalCode);
        command.Parameters.AddWithValue("@country", clientCommand.Country);

        command.Parameters.AddWithValue("@totalCost", clientCommand.TotalCost);
        command.Parameters.AddWithValue("@transportCost", clientCommand.TransportCost);

        command.Parameters.AddWithValue("@fkClient", clientCommand.Client.IDClient);

        if (!DBConnection.Execute(command))
            return false;

        clientCommand.IDClientCommand = DBConnection.GetLastPk("ClientCommand");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("ClientCommand", id);
    }

    public static bool Update(ClientCommand clientCommand)
    {
        string sql = @"UPDATE `ClientCommand`
            SET
            `commandDate`   = @commandDate,
            `commandStatus` = @commandStatus,
            `address`       = @address,
            `city`          = @city,
            `region`        = @region,
            `postalCode`    = @postalCode,
            `country`       = @country,
            `totalCost`     = @totalCost,
            `transportCost` = @transportCost,
            `fkClient`      = @fkClient,
            WHERE `pkClientCommand` = @pkClientCommand";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@pkClientCommand", clientCommand.IDClientCommand);

        command.Parameters.AddWithValue("@commandDate", clientCommand.CommandDate);
        command.Parameters.AddWithValue("@commandStatus", clientCommand.Status);

        command.Parameters.AddWithValue("@address", clientCommand.Address);
        command.Parameters.AddWithValue("@city", clientCommand.City);
        command.Parameters.AddWithValue("@region", clientCommand.Region);
        command.Parameters.AddWithValue("@postalCode", clientCommand.PostalCode);
        command.Parameters.AddWithValue("@country", clientCommand.Country);

        command.Parameters.AddWithValue("@totalCost", clientCommand.TotalCost);
        command.Parameters.AddWithValue("@transportCost", clientCommand.TransportCost);

        command.Parameters.AddWithValue("@fkClient", clientCommand.Client.IDClient);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
