using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierService
{
    public static bool GetAll(out List<Supplier>? suppliers)
    {
        string sql = @"SELECT * FROM Supplier ORDER BY IDSupplier";

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
        {
            suppliers = null;
            return false;
        }

        suppliers = new List<Supplier>();

        while (reader.Read())
        {
            suppliers.Add(new Supplier
            {
                IDSupplier = reader.GetInt32("IDSupplier"),
                Name = reader.GetString("Name"),
                Address = reader.GetString("Address"),
                PostalCode = reader.GetString("PostalCode"),
                Town = reader.GetString("Town"),
                Country = reader.GetString("Country"),
                Email = reader.GetString("Email")
            });
        }

        return true;
    }

    public static bool Get(int id, out Supplier? supplier)
    {
        string sql = $"SELECT * FROM Supplier WHERE IDSupplier = {id}";

        using MySqlCommand cmd = new MySqlCommand(sql, DBConnection.Connection);
        cmd.Parameters.AddWithValue("@id", id);

        using MySqlDataReader? reader = cmd.ExecuteReader();

        if (reader == null)
        {
            supplier = null;
            return false;
        }

        supplier = new Supplier();

        if (reader.Read())
        {
            supplier = new Supplier
            {
                IDSupplier = reader.GetInt32("IDSupplier"),
                Name = reader.GetString("Name"),
                Address = reader.GetString("Address"),
                PostalCode = reader.GetString("PostalCode"),
                Town = reader.GetString("Town"),
                Country = reader.GetString("Country"),
                Email = reader.GetString("Email")
            };
        }
        else
        {
            supplier = null;
        }

        return true;
    }

    public static bool GetAllNames(out List<SupplierName>? suppliersNames)
    {
        string sql = @"SELECT * FROM Supplier";

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
        {
            suppliersNames = null;
            return false;
        }

        suppliersNames = new List<SupplierName>();

        while (reader.Read())
        {
            suppliersNames.Add(new SupplierName
            {
                IDSupplier = reader.GetInt32("IDSupplier"),
                Name = reader.GetString("Name")
            });
        }

        return true;
    }

    public static bool Add(Supplier supplier)
    {
        string sql = "INSERT INTO Supplier " +
            "(Name, Address, PostalCode, Town, Country, Email)" +
            "VALUES (@Name, @Address, @PostalCode, @Town, @Country, @Email)";

        using (MySqlCommand command = new MySqlCommand(sql))
        {
            command.Parameters.AddWithValue("@Name", supplier.Name);
            command.Parameters.AddWithValue("@Address", supplier.Address);
            command.Parameters.AddWithValue("@PostalCode", supplier.PostalCode);
            command.Parameters.AddWithValue("@Town", supplier.Town);
            command.Parameters.AddWithValue("@Country", supplier.Country);
            command.Parameters.AddWithValue("@Email", supplier.Email);

            return DBConnection.ExecuteCommandToDB(command);
        }
    }

    public static bool Update(Supplier supplier)
    {
        string sql = @"UPDATE Supplier SET 
                        Name = @Name, 
                        Address = @Address, 
                        PostalCode = @PostalCode, 
                        Town = @Town, 
                        Country = @Country, 
                        Email = @Email
                        WHERE IDSupplier = @IDSupplier";

        using (MySqlCommand command = new MySqlCommand(sql))
        {
            command.Parameters.AddWithValue("@Name", supplier.Name);
            command.Parameters.AddWithValue("@Address", supplier.Address);
            command.Parameters.AddWithValue("@PostalCode", supplier.PostalCode);
            command.Parameters.AddWithValue("@Town", supplier.Town);
            command.Parameters.AddWithValue("@Country", supplier.Country);
            command.Parameters.AddWithValue("@Email", supplier.Email);
            command.Parameters.AddWithValue("@IDSupplier", supplier.IDSupplier);

            return DBConnection.ExecuteCommandToDB(command);
        }
    }
}
