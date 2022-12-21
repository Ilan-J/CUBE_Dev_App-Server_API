using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierService
{
    public static bool GetAll(out List<Supplier> suppliers)
    {
        string sql = "SELECT * FROM `supplier`";

        suppliers = new List<Supplier>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            suppliers.Add(new Supplier()
            {
                PkSupplier  = reader.GetInt32("pk_supplier"),

                Name        = reader.GetString("name"),
                Email       = reader.GetString("email"),

                Address     = reader.GetString("address"),
                City        = reader.GetString("city"),
                PostalCode  = reader.GetString("postal_code")
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out Supplier? supplier)
    {
        string sql = $"SELECT * FROM `supplier` " +
            $"WHERE `pk_supplier` = {id}";

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
            PkSupplier  = reader.GetInt32("pk_supplier"),

            Name        = reader.GetString("name"),
            Email       = reader.GetString("email"),

            Address     = reader.GetString("address"),
            City        = reader.GetString("city"),
            PostalCode  = reader.GetString("postal_code")
        };
        reader.Close();
        return true;
    }

    public static bool Add(Supplier supplier)
    {
        string sql = "INSERT INTO `supplier` (`name`, `email`, `address`, `city`, `postal_code`) " +
            "VALUES (@name, @email, @address, @city, @postal_code)";
        
        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",        supplier.Name);
        command.Parameters.AddWithValue("@email",       supplier.Email);

        command.Parameters.AddWithValue("@address",     supplier.Address);
        command.Parameters.AddWithValue("@city",        supplier.City);
        command.Parameters.AddWithValue("@postal_code", supplier.PostalCode);

        if (!DBConnection.Execute(command))
            return false;

        supplier.PkSupplier = DBConnection.GetLastPk("supplier");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("supplier", id);
    }

    public static bool Update(Supplier supplier)
    {
        string sql = $"UPDATE `supplier` " +
            $"SET `name` = @name, `email` = @email, `address` = @address, `city` = @city, `postal_code` = @postal_code " +
            $"WHERE `pk_supplier` = {supplier.PkSupplier}";
        
        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",        supplier.Name);
        command.Parameters.AddWithValue("@email",       supplier.Email);

        command.Parameters.AddWithValue("@address",     supplier.Address);
        command.Parameters.AddWithValue("@city",        supplier.City);
        command.Parameters.AddWithValue("@postal_code", supplier.PostalCode);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
