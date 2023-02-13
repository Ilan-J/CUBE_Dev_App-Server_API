using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ClientService
{
    public static bool GetAll(out List<Client_DTO>? clients)
    {
        string sql = @"SELECT * FROM Client";

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
        {
            clients = null;
            return false;
        }

        clients = new List<Client_DTO>();

        while (reader.Read())
        {
            clients.Add(new Client_DTO
            {
                IDClient = reader.GetInt32("IDClient"),
                Firstname = reader.GetString("Firstname"),
                Lastname = reader.GetString("Lastname"),
                Address = reader.GetString("Address"),
                PostalCode = reader.GetString("PostalCode"),
                Town = reader.GetString("Town"),
                Country = reader.GetString("Country"),
                Email = reader.GetString("Email")
            });
        }

        return true;

    }

    public static bool Get(int id, out Client_DTO? client)
    {
        string sql = $"SELECT * FROM Client WHERE IDClient = {id}";

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
        {
            client = null;
            return false;
        }

        if (reader.Read())
        {
            client = new Client_DTO
            {
                IDClient = reader.GetInt32("IDClient"),
                Firstname = reader.GetString("Firstname"),
                Lastname = reader.GetString("Lastname"),
                Address = reader.GetString("Address"),
                PostalCode = reader.GetString("PostalCode"),
                Town = reader.GetString("Town"),
                Country = reader.GetString("Country"),
                Email = reader.GetString("Email")
            };
        }
        else
        {
            client = null;
        }

        return true;
    }

    public static bool Add(Client client)
    {
        string sql = "INSERT INTO Client " +
            "(Firstname, Lastname, Address, PostalCode, Town, Country, Email, Password)" +
            "VALUES (@Firstname, @Lastname, @Address, @PostalCode, @Town, @Country, @Email, @Password)";

        using MySqlCommand command = new MySqlCommand(sql);

        command.Parameters.AddWithValue("@Firstname", client.Firstname);
        command.Parameters.AddWithValue("@Lastname", client.Lastname);
        command.Parameters.AddWithValue("@Address", client.Address);
        command.Parameters.AddWithValue("@PostalCode", client.PostalCode);
        command.Parameters.AddWithValue("@Town", client.Town);
        command.Parameters.AddWithValue("@Country", client.Country);
        command.Parameters.AddWithValue("@Email", client.Email);
        command.Parameters.AddWithValue("@Password", client.Password);

        return DBConnection.ExecuteCommandToDB(command);
    }

    public static bool Update(Client client)
    {
        string sql = @"UPDATE Client SET 
                    Firstname = @Firstname, 
                    Lastname = @Lastname, 
                    Address = @Address, 
                    PostalCode = @PostalCode, 
                    Town = @Town, 
                    Country = @Country, 
                    Email = @Email,
                    Password = @Password
                    WHERE IDClient = @IDClient";

        using MySqlCommand command = new MySqlCommand(sql);

        command.Parameters.AddWithValue("@Firstname", client.Firstname);
        command.Parameters.AddWithValue("@Lastname", client.Lastname);
        command.Parameters.AddWithValue("@Address", client.Address);
        command.Parameters.AddWithValue("@PostalCode", client.PostalCode);
        command.Parameters.AddWithValue("@Town", client.Town);
        command.Parameters.AddWithValue("@Country", client.Country);
        command.Parameters.AddWithValue("@Email", client.Email);
        command.Parameters.AddWithValue("@Password", client.Password);
        command.Parameters.AddWithValue("@IDClient", client.IDClient);

        return DBConnection.ExecuteCommandToDB(command);
    }

    public static bool Delete(int id)
    {
        string sql = $"DELETE FROM Client WHERE IDClient = {id}";

        using MySqlCommand command = new MySqlCommand(sql);

        return DBConnection.ExecuteCommandToDB(command);
    }
}
