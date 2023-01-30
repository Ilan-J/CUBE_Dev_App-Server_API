using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ProductService
{
    public static bool GetAll(out List<Article> products)
    {
        string sql = "SELECT * FROM `Product`";

        products = new List<Article>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            products.Add(new Article()
            {
                IDArticle       = reader.GetInt32("pkProduct"),

                Name            = reader.GetString("name"),
                Reference       = reader.GetString("reference"),

                BuyingPrice           = reader.GetInt32("price"),
                TVA             = reader.GetInt32("tva"),

                WineYear             = reader.GetInt32("age"),
                Description     = reader.GetString("description"),

                Quantity           = reader.GetInt32("stock"),
                MinQuantity        = reader.GetInt32("stockMin"),

                WineFamily = new WineFamily()
                {
                    IDWineFamily = reader.GetInt32("fkWineFamily")
                },
                Supplier = new Supplier()
                {
                    IDSupplier = reader.GetInt32("fkSupplier")
                }
            });
        }
        reader.Close();

        if (!WineFamilyService.GetAll(out List<WineFamily> wineFamilies))
            return false;

        if (!SupplierService.GetAll(out List<Supplier> suppliers))
            return false;

        products.ForEach(product =>
        {
            product.WineFamily  = wineFamilies[ product.WineFamily.IDWineFamily - 1];
            product.Supplier    = suppliers[    product.Supplier.IDSupplier - 1];
        });
        return true;
    }

    public static bool Get(int id, out Article? product)
    {
        string sql = $"SELECT * FROM `Product` WHERE `pkProduct` = {id}";

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
        {
            product = null;
            return false;
        }

        if (!reader.Read())
        {
            product = null;
            return true;
        }
        
        product = new Article()
        {
            IDArticle       = reader.GetInt32("pkProduct"),

            Name            = reader.GetString("name"),
            Reference       = reader.GetString("reference"),

            BuyingPrice           = reader.GetInt32("price"),
            TVA             = reader.GetInt32("tva"),

            WineYear             = reader.GetInt32("age"),
            Description     = reader.GetString("description"),

            Quantity           = reader.GetInt32("stock"),
            MinQuantity        = reader.GetInt32("stockMin"),

            WineFamily = new WineFamily()
            {
                IDWineFamily = reader.GetInt32("fkWineFamily")
            },
            Supplier = new Supplier()
            {
                IDSupplier = reader.GetInt32("fkSupplier")
            }
        };
        reader.Close();

        if (!WineFamilyService.Get(product.WineFamily.IDWineFamily, out WineFamily? wineFamily))
            return false;
        if (wineFamily is null)
            return false;
        product.WineFamily = wineFamily;

        if (!SupplierService.Get(product.Supplier.IDSupplier, out Supplier? supplier))
            return false;
        if (supplier is null)
            return false;
        product.Supplier = supplier;

        return true;
    }

    public static bool Add(Article product)
    {
        string sql = "INSERT INTO `Product` (name, reference, price, tva, age, description, stock, stockMin, fkWineFamily, fkSupplier) " +
            "VALUES (@name, @reference, @price, @tva, @age, @description, @stock, @stockMin, @fkWineFamily, @fkSupplier)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",            product.Name);
        command.Parameters.AddWithValue("@reference",       product.Reference);

        command.Parameters.AddWithValue("@price",           product.BuyingPrice);
        command.Parameters.AddWithValue("@tva",             product.TVA);

        command.Parameters.AddWithValue("@age",             product.WineYear);
        command.Parameters.AddWithValue("@description",     product.Description);

        command.Parameters.AddWithValue("@stock",           product.Quantity);
        command.Parameters.AddWithValue("@stockMin",        product.MinQuantity);

        command.Parameters.AddWithValue("@fkWineFamily",    product.WineFamily.IDWineFamily);
        command.Parameters.AddWithValue("@fkSupplier",      product.Supplier.IDSupplier);

        if (!DBConnection.Execute(command))
            return false;

        product.IDArticle = DBConnection.GetLastPk("Product");

        if (!WineFamilyService.Get(product.WineFamily.IDWineFamily, out WineFamily? wineFamily))
            return false;
        if (wineFamily is null)
            return false;
        product.WineFamily = wineFamily;

        if (!SupplierService.Get(product.Supplier.IDSupplier, out Supplier? supplier))
            return false;
        if (supplier is null)
            return false;
        product.Supplier = supplier;

        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("Product", id);
    }

    public static bool Update(Article product)
    {
        string sql = $"UPDATE `Product`             " +
            $"SET                                   " +
            $"`name`            = @name,            " +
            $"`reference`       = @reference,       " +
            $"`price`           = @price,           " +
            $"`tva`             = @tva,             " +
            $"`age`             = @age,             " +
            $"`description`     = @description,     " +
            $"`stock`           = @stock,           " +
            $"`stockMin`        = @stockMin,        " +
            $"`fkWineFamily`    = @fkWineFamily,    " +
            $"`fkSupplier`      = @fkSupplier       " +
            $"WHERE `pkProduct` = {product.IDArticle}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",            product.Name);
        command.Parameters.AddWithValue("@reference",       product.Reference);

        command.Parameters.AddWithValue("@price",           product.BuyingPrice);
        command.Parameters.AddWithValue("@tva",             product.TVA);

        command.Parameters.AddWithValue("@age",             product.WineYear);
        command.Parameters.AddWithValue("@description",     product.Description);

        command.Parameters.AddWithValue("@stock",           product.Quantity);
        command.Parameters.AddWithValue("@stockMin",        product.MinQuantity);

        command.Parameters.AddWithValue("@fkWineFamily",    product.WineFamily.IDWineFamily);
        command.Parameters.AddWithValue("@fkSupplier",      product.Supplier.IDSupplier);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
