using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierService
{
    public static List<Supplier> GetAll()
    {
        if (!DBConnection.GetConnection(out MySqlConnection connection))
        {
            throw new Exception("Cannot connect to server");
        }
        string sql = "SELECT * FROM `supplier`";

        MySqlDataReader reader = new MySqlCommand(sql, connection).ExecuteReader();

        List<Supplier> suppliers = new();
        while (reader.Read())
        {
            suppliers.Add(new Supplier
            {
                Id =            reader.GetInt32("id"),
                Name =          reader.GetString("name"),
                Email =         reader.GetString("email"),
                Address =       reader.GetString("address"),
                City =          reader.GetString("city"),
                PostalCode =    reader.GetString("postalcode")
            });
        }
        reader.Close();
        connection.Close();

        return suppliers;
    }

    public static Supplier? Get(int id)
    {
        if (!DBConnection.GetConnection(out MySqlConnection connection))
        {
            throw new Exception("Cannot connect to server");
        }
        string sql = $"SELECT * FROM `supplier` WHERE `id` = {id}";

        MySqlDataReader reader = new MySqlCommand(sql, connection).ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }
        Supplier supplier = new()
        {
            Id =            reader.GetInt32("id"),
            Name =          reader.GetString("name"),
            Email =         reader.GetString("email"),
            Address =       reader.GetString("address"),
            City =          reader.GetString("city"),
            PostalCode =    reader.GetString("postalcode")
        };
        reader.Close();
        connection.Close();

        return supplier;
    }

    public static void Add(Supplier supplier)
    {
        if (!DBConnection.GetConnection(out MySqlConnection connection))
        {
            throw new Exception("Cannot connect to server");
        }
        string sql = "INSERT INTO `supplier` (`name`, `email`, `address`, `city`, `postalcode`) " +
            "VALUES (@name, @email, @address, @city, @postalcode)";
        
        MySqlCommand command = new(sql, connection);

        command.Parameters.AddWithValue("@name", supplier.Name);
        command.Parameters.AddWithValue("@email", supplier.Email);
        command.Parameters.AddWithValue("@address", supplier.Address);
        command.Parameters.AddWithValue("@city", supplier.City);
        command.Parameters.AddWithValue("@postalcode", supplier.PostalCode);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void Delete(int id)
    {
        if (!DBConnection.GetConnection(out MySqlConnection connection))
        {
            throw new Exception("Cannot connect to server");
        }
        string sql = $"DELETE FROM `supplier` WHERE `id` = {id}";

        new MySqlCommand(sql, connection).ExecuteNonQuery();
        connection.Close();
    }

    public static void Update(Supplier supplier)
    {
        if (!DBConnection.GetConnection(out MySqlConnection connection))
        {
            throw new Exception("Cannot connect to server");
        }
        string sql = $"UPDATE `supplier` " +
            $"SET `name` = @name, `email` = @email, `address` = @address, `city` = @city, `postalcode` = @postalcode " +
            $"WHERE `id` = {supplier.Id}";

        MySqlCommand command = new(sql, connection);

        command.Parameters.AddWithValue("@name", supplier.Name);
        command.Parameters.AddWithValue("@email", supplier.Email);
        command.Parameters.AddWithValue("@address", supplier.Address);
        command.Parameters.AddWithValue("@city", supplier.City);
        command.Parameters.AddWithValue("@postalcode", supplier.PostalCode);

        command.ExecuteNonQuery();
        connection.Close();
    }
}
