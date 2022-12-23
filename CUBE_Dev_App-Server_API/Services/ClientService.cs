using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ClientService
{
    public static bool GetAll(out List<Client> clients)
    {
        string sql = "SELECT * FROM `Client`";

        clients = new List<Client>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            clients.Add(new Client()
            {
                PkClient    = reader.GetInt32("pkClient"),

                Email       = reader.GetString("email"),
                Password    = reader.GetString("password"),

                Firstname   = reader.GetString("firstname"),
                Lastname    = reader.GetString("lastname"),

                Address     = reader.GetString("address"),
                City        = reader.GetString("city"),
                Region      = reader.GetString("region"),
                PostalCode  = reader.GetString("postalCode"),
                Country     = reader.GetString("country")
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out Client? client)
    {
        string sql = $"SELECT * FROM `Client` WHERE `pkClient` = {id}";

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
        {
            client = null;
            return false;
        }
        if (!reader.Read())
        {
            reader.Close();
            client = null;
            return false;
        }

        client = new Client()
        {
            PkClient    = reader.GetInt32("pkClient"),

            Email       = reader.GetString("email"),
            Password    = reader.GetString("password"),

            Firstname   = reader.GetString("firstname"),
            Lastname    = reader.GetString("lastname"),

            Address     = reader.GetString("address"),
            City        = reader.GetString("city"),
            Region      = reader.GetString("region"),
            PostalCode  = reader.GetString("postalCode"),
            Country     = reader.GetString("country")
        };
        reader.Close();
        return true;
    }

    public static bool Add(Client client)
    {
        string sql = "INSERT INTO `Client` (email, password, firstname, lastname, address, city, region, postalCode, country) " +
            "VALUES (@email, @password, @firstname, @lastname, @address, @city, @region, @postalCode, @country)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@email",       client.Email);
        command.Parameters.AddWithValue("@password",    client.Password);

        command.Parameters.AddWithValue("@firstname",   client.Firstname);
        command.Parameters.AddWithValue("@lastname",    client.Lastname);

        command.Parameters.AddWithValue("@address",     client.Address);
        command.Parameters.AddWithValue("@city",        client.City);
        command.Parameters.AddWithValue("@region",      client.Region);
        command.Parameters.AddWithValue("@postalCode",  client.PostalCode);
        command.Parameters.AddWithValue("@country",     client.Country);

        if (!DBConnection.Execute(command))
            return false;

        client.PkClient = DBConnection.GetLastPk("Client");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("Client", id);
    }

    public static bool Update(Client client)
    {
        string sql = $@"UPDATE `Client`
            SET
            `email`           = @email,
            `password`        = @password,
            `firstname`       = @firstname,
            `lastname`        = @lastname,
            `address`         = @address,
            `city`            = @city,
            `region`          = @region,
            `postalCode`      = @postalCode,
            `country`         = @country
            WHERE `pkClient`  = {client.PkClient}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@email",       client.Email);
        command.Parameters.AddWithValue("@password",    client.Password);

        command.Parameters.AddWithValue("@firstname",   client.Firstname);
        command.Parameters.AddWithValue("@lastname",    client.Lastname);

        command.Parameters.AddWithValue("@address",     client.Address);
        command.Parameters.AddWithValue("@city",        client.City);
        command.Parameters.AddWithValue("@region",      client.Region);
        command.Parameters.AddWithValue("@postalCode",  client.PostalCode);
        command.Parameters.AddWithValue("@country",     client.Country);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
