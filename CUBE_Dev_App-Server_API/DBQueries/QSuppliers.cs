using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.DBQueries;

public static class QSuppliers
{
    public static Supplier[]? SelectAll()
    {
        if (!DBConnection.GetConnection(out MySqlConnection connection))
        {
            return null;
        }
        string sql = "SELECT * FROM `supplier`";

        MySqlCommand command = new(sql, connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Supplier> supplierList = new();
        while (reader.Read())
        {
            supplierList.Add(new Supplier(
                reader.GetInt32("pk_supplier"),
                reader.GetString("name"),
                reader.GetString("email"),
                reader.GetString("address"),
                reader.GetString("city"),
                reader.GetString("postalcode")
                ));
        }
        reader.Close();
        connection.Close();

        return supplierList.ToArray();
    }
    public static bool Select(int pk_supplier, out Supplier? supplier)
    {
        if (!DBConnection.GetConnection(out MySqlConnection connection))
        {
            supplier = null;
            return false;
        }
        string sql = $"SELECT * FROM `supplier` WHERE `pk_supplier` = {pk_supplier}";

        MySqlCommand command = new(sql, connection);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        supplier = new Supplier(
            reader.GetInt32("pk_supplier"),
            reader.GetString("name"),
            reader.GetString("email"),
            reader.GetString("address"),
            reader.GetString("city"),
            reader.GetString("postalcode")
            );
        reader.Close();
        connection.Close();

        return true;
    }
    /*public static IEnumerable<Supplier> Insert()
    {

    }*/
}
