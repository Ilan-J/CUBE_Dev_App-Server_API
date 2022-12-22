using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplyListService
{
    public static bool GetAll(out List<SupplyList> supplyLists)
    {
        string sql = "SELECT * FROM `SupplyList`";

        supplyLists = new List<SupplyList>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            supplyLists.Add(new SupplyList()
            {
                SupplierCommand = new SupplierCommand()
                {
                    PkSupplierCommand = reader.GetInt32("fkSupplierCommand")
                },
                Product = new Product()
                {
                    PkProduct = reader.GetInt32("fkProduct")
                },
                Quantity = reader.GetInt32("quantity")
            });
        }
        reader.Close();
        return true;
    }

    public static bool GetFromSupplier(int id, out List<SupplyList> supplyLists)
    {
        string sql = $"SELECT * FROM `SupplyList` WHERE `fkSupplierCommand` = {id}";

        supplyLists = new List<SupplyList>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            supplyLists.Add(new SupplyList()
            {
                SupplierCommand = new SupplierCommand()
                {
                    PkSupplierCommand = reader.GetInt32("fkSupplierCommand")
                },
                Product = new Product()
                {
                    PkProduct = reader.GetInt32("fkProduct")
                },
                Quantity = reader.GetInt32("quantity")
            });
        }
        reader.Close();
        return true;
    }
    public static bool GetFromProduct(int id, out List<SupplyList> supplyLists)
    {
        string sql = $"SELECT * FROM `SupplyList` WHERE `fkProduct` = {id}";

        supplyLists = new List<SupplyList>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            supplyLists.Add(new SupplyList()
            {
                SupplierCommand = new SupplierCommand()
                {
                    PkSupplierCommand = reader.GetInt32("fkSupplierCommand")
                },
                Product = new Product()
                {
                    PkProduct = reader.GetInt32("fkProduct")
                },
                Quantity = reader.GetInt32("quantity")
            });
        }
        reader.Close();
        return true;
    }

    public static bool Add(SupplyList supply)
    {
        string sql = "INSERT INTO `SupplyList` (`fkSupplierCommand`, `fkProduct`, `quantity`) " +
            "VALUES (@fkSupplierCommand, @fkProduct, @quantity)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@fkSupplierCommand",   supply.SupplierCommand.PkSupplierCommand);
        command.Parameters.AddWithValue("@fkProduct",           supply.Product.PkProduct);
        command.Parameters.AddWithValue("@quantity",            supply.Quantity);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("SupplyList", id);
    }

    public static bool Update(SupplyList supply)
    {
        string sql = $"UPDATE `SupplyList` " +
            $"SET `fkSupplierCommand` = @fkSupplierCommand, `fkProduct` = @fkProduct, `quantity` = @quantity " +
            $"WHERE `fkSupplierCommand` = {supply.SupplierCommand.PkSupplierCommand} AND `fkProduct` = {supply.Product.PkProduct}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@fkSupplierCommand",   supply.SupplierCommand.PkSupplierCommand);
        command.Parameters.AddWithValue("@fkProduct",           supply.Product.PkProduct);
        command.Parameters.AddWithValue("@quantity",            supply.Quantity);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
