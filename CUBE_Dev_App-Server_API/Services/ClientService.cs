using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ClientService
{
    public static List<Client> GetAll()
    {
        string sql = "SELECT * FROM `client`";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        List<Client> clients = new();
        while (reader.Read())
        {
            clients.Add(new Client()
            {
                PkClient =      reader.GetInt32("pk_client"),

                Email =         reader.GetString("email"),
                Password =      reader.GetString("password"),

                Firstname =     reader.GetString("firstname"),
                Lastname =      reader.GetString("lastname"),

                Address =       reader.GetString("address"),
                City =          reader.GetString("city"),
                Region =        reader.GetString("region"),
                PostalCode =    reader.GetString("postal_code"),
                Country =       reader.GetString("country")
            });
        }
        DBConnection.Connection.Close();

        return clients;
    }

    public static Client? Get(int id)
    {
        string sql = $"SELECT * FROM `client` " +
            $"WHERE `pk_client` = {id}";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        if (!reader.Read())
            return null;

        Client client = new()
        {
            PkClient =      reader.GetInt32("pk_client"),

            Email =         reader.GetString("email"),
            Password =      reader.GetString("password"),

            Firstname =     reader.GetString("firstname"),
            Lastname =      reader.GetString("lastname"),

            Address =       reader.GetString("address"),
            City =          reader.GetString("city"),
            Region =        reader.GetString("region"),
            PostalCode =    reader.GetString("postal_code"),
            Country =       reader.GetString("country")
        };
        DBConnection.Connection.Close();

        return client;
    }

    public static void Add(Client client)
    {
        string sql = "INSERT INTO `client` (email, password, firstname, lastname, address, city, region, postal_code, country) " +
            "VALUES (@email, @password, @firstname, @lastname, @address, @city, @region, @postal_code, @country)";

        MySqlCommand command = new(sql, DBConnection.Connection);

        command.Parameters.AddWithValue("@email",       client.Email);
        command.Parameters.AddWithValue("@password",    client.Password);

        command.Parameters.AddWithValue("@firstname",   client.Firstname);
        command.Parameters.AddWithValue("@lastname",    client.Lastname);

        command.Parameters.AddWithValue("@address",     client.Address);
        command.Parameters.AddWithValue("@city",        client.City);
        command.Parameters.AddWithValue("@region",      client.Region);
        command.Parameters.AddWithValue("@postal_code", client.PostalCode);
        command.Parameters.AddWithValue("@country",     client.Country);

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM `client` " +
            $"WHERE `client` = {id}";

        DBConnection.Connection.Open();
        new MySqlCommand(sql, DBConnection.Connection).ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Update(Client client)
    {
        string sql = $"UPDATE `client` " +
            $"SET `email` = @email, `password` = @password, `firstname` = @firstname, `lastname` = @lastname, `address` = @address, `city` = @city, `region` = @region, `postal_code` = @postal_code, `country` = @country " +
            $"WHERE `pk_client` = {client.PkClient}";

        MySqlCommand command = new(sql, DBConnection.Connection);

        command.Parameters.AddWithValue("@email",       client.Email);
        command.Parameters.AddWithValue("@password",    client.Password);

        command.Parameters.AddWithValue("@firstname",   client.Firstname);
        command.Parameters.AddWithValue("@lastname",    client.Lastname);

        command.Parameters.AddWithValue("@address",     client.Address);
        command.Parameters.AddWithValue("@city",        client.City);
        command.Parameters.AddWithValue("@region",      client.Region);
        command.Parameters.AddWithValue("@postal_code", client.PostalCode);
        command.Parameters.AddWithValue("@country",     client.Country);

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }
}
