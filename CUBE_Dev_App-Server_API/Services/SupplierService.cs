using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierService
{
    public static bool GetAll(out List<Supplier> suppliers)
    {
        string sql = "SELECT * FROM `Supplier`";

        suppliers = new List<Supplier>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            suppliers.Add(new Supplier()
            {
                IDSupplier = reader.GetInt32("pkSupplier"),

                Name = reader.GetString("name"),
                Email = reader.GetString("email"),

                Address = reader.GetString("address"),
                Town = reader.GetString("city"),
                PostalCode = reader.GetString("postalCode")
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out Supplier? supplier)
    {
        string sql = $"SELECT * FROM `Supplier` WHERE `pkSupplier` = {id}";

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
        {
            supplier = null;
            return false;
        }

        if (!reader.Read())
        {
            supplier = null;
            return false;
        }

        supplier = new Supplier()
        {
            IDSupplier = reader.GetInt32("pkSupplier"),

            Name = reader.GetString("name"),
            Email = reader.GetString("email"),

            Address = reader.GetString("address"),
            Town = reader.GetString("city"),
            PostalCode = reader.GetString("postalCode")
        };
        reader.Close();
        return true;
    }

    public static bool Add(Supplier supplier)
    {
        string sql = "INSERT INTO `Supplier` (`name`, `email`, `address`, `city`, `postalCode`) " +
            "VALUES (@name, @email, @address, @city, @postalCode)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name", supplier.Name);
        command.Parameters.AddWithValue("@email", supplier.Email);

        command.Parameters.AddWithValue("@address", supplier.Address);
        command.Parameters.AddWithValue("@city", supplier.Town);
        command.Parameters.AddWithValue("@postalCode", supplier.PostalCode);

        if (!DBConnection.Execute(command))
            return false;

        supplier.IDSupplier = DBConnection.GetLastPk("Supplier");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("Supplier", id);
    }

    public static bool Update(Supplier supplier)
    {
        string sql = $"UPDATE `Supplier` " +
            $"SET `name` = @name, `email` = @email, `address` = @address, `city` = @city, `postalCode` = @postalCode " +
            $"WHERE `pkSupplier` = {supplier.IDSupplier}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name", supplier.Name);
        command.Parameters.AddWithValue("@email", supplier.Email);

        command.Parameters.AddWithValue("@address", supplier.Address);
        command.Parameters.AddWithValue("@city", supplier.Town);
        command.Parameters.AddWithValue("@postalCode", supplier.PostalCode);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
