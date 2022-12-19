using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierService
{
    public static List<Supplier> GetAll()
    {
        string sql = "SELECT * FROM `supplier`";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        List<Supplier> suppliers = new();
        while (reader.Read())
        {
            suppliers.Add(new Supplier
            {
                PkSupplier =    reader.GetInt32("pk_supplier"),

                Name =          reader.GetString("name"),
                Email =         reader.GetString("email"),

                Address =       reader.GetString("address"),
                City =          reader.GetString("city"),
                PostalCode =    reader.GetString("postal_code")
            });
        }
        DBConnection.Connection.Close();

        return suppliers;
    }

    public static Supplier? Get(int id)
    {
        string sql = $"SELECT * FROM `supplier` " +
            $"WHERE `pk_supplier` = {id}";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        if (!reader.Read())
            return null;

        Supplier supplier = new()
        {
            PkSupplier =    reader.GetInt32("pk_supplier"),

            Name =          reader.GetString("name"),
            Email =         reader.GetString("email"),

            Address =       reader.GetString("address"),
            City =          reader.GetString("city"),
            PostalCode =    reader.GetString("postal_code")
        };
        DBConnection.Connection.Close();

        return supplier;
    }

    public static void Add(Supplier supplier)
    {
        string sql = "INSERT INTO `supplier` (`name`, `email`, `address`, `city`, `postal_code`) " +
            "VALUES (@name, @email, @address, @city, @postal_code)";
        
        MySqlCommand command = new(sql, DBConnection.Connection);

        command.Parameters.AddWithValue("@name",        supplier.Name);
        command.Parameters.AddWithValue("@email",       supplier.Email);

        command.Parameters.AddWithValue("@address",     supplier.Address);
        command.Parameters.AddWithValue("@city",        supplier.City);
        command.Parameters.AddWithValue("@postal_code", supplier.PostalCode);

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM `supplier` " +
            $"WHERE `pk_supplier` = {id}";

        DBConnection.Connection.Open();
        new MySqlCommand(sql, DBConnection.Connection).ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Update(Supplier supplier)
    {
        string sql = $"UPDATE `supplier` " +
            $"SET `name` = @name, `email` = @email, `address` = @address, `city` = @city, `postal_code` = @postal_code " +
            $"WHERE `pk_supplier` = {supplier.PkSupplier}";
        
        MySqlCommand command = new(sql, DBConnection.Connection);

        command.Parameters.AddWithValue("@name",        supplier.Name);
        command.Parameters.AddWithValue("@email",       supplier.Email);

        command.Parameters.AddWithValue("@address",     supplier.Address);
        command.Parameters.AddWithValue("@city",        supplier.City);
        command.Parameters.AddWithValue("@postal_code", supplier.PostalCode);

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }
}
