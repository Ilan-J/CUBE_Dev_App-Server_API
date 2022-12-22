using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ProductService
{
    public static bool GetAll(out List<Product> products)
    {
        string sql = "SELECT * FROM `Product`";

        products = new List<Product>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            products.Add(new Product()
            {
                PkProduct       = reader.GetInt32("pkProduct"),

                Name            = reader.GetString("name"),
                Reference       = reader.GetString("reference"),

                Price           = reader.GetInt32("price"),
                TVA             = reader.GetInt32("tva"),

                Age             = reader.GetInt32("age"),
                Description     = reader.GetString("description"),

                Stock           = reader.GetInt32("stock"),
                StockMin        = reader.GetInt32("stockMin"),

                WineFamily = new WineFamily()
                {
                    PkWineFamily = reader.GetInt32("fkWineFamily")
                },
                Supplier = new Supplier()
                {
                    PkSupplier = reader.GetInt32("fkSupplier")
                }
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out Product? product)
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
        
        product = new Product()
        {
            PkProduct       = reader.GetInt32("pkProduct"),

            Name            = reader.GetString("name"),
            Reference       = reader.GetString("reference"),

            Price           = reader.GetInt32("price"),
            TVA             = reader.GetInt32("tva"),

            Age             = reader.GetInt32("age"),
            Description     = reader.GetString("description"),

            Stock           = reader.GetInt32("stock"),
            StockMin        = reader.GetInt32("stockMin"),

            WineFamily = new WineFamily()
            {
                PkWineFamily = reader.GetInt32("fkWineFamily")
            },
            Supplier = new Supplier()
            {
                PkSupplier = reader.GetInt32("fkSupplier")
            }
        };
        reader.Close();
        return true;
    }

    public static bool Add(Product product)
    {
        string sql = "INSERT INTO `Product` (name, reference, price, tva, age, description, stock, stockMin, fkWineFamily, fkSupplier) " +
            "VALUES (@name, @reference, @price, @tva, @age, @description, @stock, @stockMin, @fkWineFamily, @fkSupplier)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",            product.Name);
        command.Parameters.AddWithValue("@reference",       product.Reference);

        command.Parameters.AddWithValue("@price",           product.Price);
        command.Parameters.AddWithValue("@tva",             product.TVA);

        command.Parameters.AddWithValue("@age",             product.Age);
        command.Parameters.AddWithValue("@description",     product.Description);

        command.Parameters.AddWithValue("@stock",           product.Stock);
        command.Parameters.AddWithValue("@stockMin",        product.StockMin);

        command.Parameters.AddWithValue("@fkWineFamily",    product.WineFamily.PkWineFamily);
        command.Parameters.AddWithValue("@fkSupplier",      product.Supplier.PkSupplier);

        if (!DBConnection.Execute(command))
            return false;

        product.PkProduct = DBConnection.GetLastPk("Product");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("Product", id);
    }

    public static bool Update(Product product)
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
            $"WHERE `pkProduct` = {product.PkProduct}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",            product.Name);
        command.Parameters.AddWithValue("@reference",       product.Reference);

        command.Parameters.AddWithValue("@price",           product.Price);
        command.Parameters.AddWithValue("@tva",             product.TVA);

        command.Parameters.AddWithValue("@age",             product.Age);
        command.Parameters.AddWithValue("@description",     product.Description);

        command.Parameters.AddWithValue("@stock",           product.Stock);
        command.Parameters.AddWithValue("@stockMin",        product.StockMin);

        command.Parameters.AddWithValue("@fkWineFamily",    product.WineFamily.PkWineFamily);
        command.Parameters.AddWithValue("@fkSupplier",      product.Supplier.PkSupplier);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
