using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ProductService
{
    public static bool GetAll(out List<Product> products)
    {
        string sql = "SELECT * FROM `product`";

        products = new List<Product>();

        MySqlDataReader? reader = DBConnection.ExecuteReader(sql);
        if (reader is null)
            return false;

        while (reader.Read())
        {
            products.Add(new Product()
            {
                PkProduct =     reader.GetInt32("pk_product"),

                Name =          reader.GetString("name"),
                Reference =     reader.GetString("reference"),

                Price =         reader.GetInt32("price"),
                TVA =           reader.GetInt32("tva"),

                Age =           reader.GetInt32("age"),
                Description =   reader.GetString("description"),

                Stock =         reader.GetInt32("stock"),
                StockMin =      reader.GetInt32("stock_min"),

                FkWineFamily =  reader.GetInt32("fk_wine_family"),
                FkSupplier =    reader.GetInt32("fk_supplier")
            });
        }
        reader.Close();
        return true;
    }

    public static bool Get(int id, out Product? product)
    {
        string sql = $"SELECT * FROM `product` " +
            $"WHERE `pk_product` = {id}";

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
            PkProduct =     reader.GetInt32("pk_product"),

            Name =          reader.GetString("name"),
            Reference =     reader.GetString("reference"),

            Price =         reader.GetInt32("price"),
            TVA =           reader.GetInt32("tva"),

            Age =           reader.GetInt32("age"),
            Description =   reader.GetString("description"),

            Stock =         reader.GetInt32("stock"),
            StockMin =      reader.GetInt32("stock_min"),

            FkWineFamily =  reader.GetInt32("fk_wine_family"),
            FkSupplier =    reader.GetInt32("fk_supplier")
        };
        reader.Close();
        return true;
    }

    public static bool Add(Product product)
    {
        string sql = "INSERT INTO `product` (name, reference, price, tva, age, description, stock, stock_min, fk_wine_family, fk_supplier) " +
            "VALUES (@name, @reference, @price, @tva, @age, @description, @stock, @stock_min, @fk_wine_family, @fk_supplier)";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",            product.Name);
        command.Parameters.AddWithValue("@reference",       product.Reference);

        command.Parameters.AddWithValue("@price",           product.Price);
        command.Parameters.AddWithValue("@tva",             product.TVA);

        command.Parameters.AddWithValue("@age",             product.Age);
        command.Parameters.AddWithValue("@description",     product.Description);

        command.Parameters.AddWithValue("@stock",           product.Stock);
        command.Parameters.AddWithValue("@stock_min",       product.StockMin);

        command.Parameters.AddWithValue("@fk_wine_family",  product.FkWineFamily);
        command.Parameters.AddWithValue("@fk_supplier",     product.FkSupplier);

        if (!DBConnection.Execute(command))
            return false;

        product.PkProduct = DBConnection.GetLastPk("product");
        return true;
    }

    public static bool Delete(int id)
    {
        return DBConnection.Delete("product", id);
    }

    public static bool Update(Product product)
    {
        string sql = $"UPDATE `product`                 " +
            $"SET                                       " +
            $"`name` =              @name,              " +
            $"`reference` =         @reference,         " +
            $"`price` =             @price,             " +
            $"`tva` =               @tva,               " +
            $"`age` =               @age,               " +
            $"`description` =       @description,       " +
            $"`stock` =             @stock,             " +
            $"`stock_min` =         @stock_min,         " +
            $"`fk_wine_family` =    @fk_wine_family,    " +
            $"`fk_supplier` =       @fk_supplier        " +
            $"WHERE `pk_product` =  {product.PkProduct}";

        MySqlCommand command = new(sql);

        command.Parameters.AddWithValue("@name",            product.Name);
        command.Parameters.AddWithValue("@reference",       product.Reference);

        command.Parameters.AddWithValue("@price",           product.Price);
        command.Parameters.AddWithValue("@tva",             product.TVA);

        command.Parameters.AddWithValue("@age",             product.Age);
        command.Parameters.AddWithValue("@description",     product.Description);

        command.Parameters.AddWithValue("@stock",           product.Stock);
        command.Parameters.AddWithValue("@stock_min",       product.StockMin);

        command.Parameters.AddWithValue("@fk_wine_family",  product.FkWineFamily);
        command.Parameters.AddWithValue("@fk_supplier",     product.FkSupplier);

        if (!DBConnection.Execute(command))
            return false;

        return true;
    }
}
