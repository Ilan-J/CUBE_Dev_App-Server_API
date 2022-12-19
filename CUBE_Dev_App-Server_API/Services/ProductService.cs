using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ProductService
{
    public static List<Product> GetAll()
    {
        string sql = "SELECT * FROM `product`";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        List<Product> products = new();
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
        DBConnection.Connection.Close();

        return products;
    }

    public static Product? Get(int id)
    {
        string sql = $"SELECT * FROM `product` " +
            $"WHERE `pk_product` = {id}";

        DBConnection.Connection.Open();
        MySqlDataReader reader = new MySqlCommand(sql, DBConnection.Connection).ExecuteReader();

        if (!reader.Read())
            return null;
        
        Product product = new()
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
        DBConnection.Connection.Close();

        return product;
    }

    public static void Add(Product product)
    {
        string sql = "INSERT INTO `product` (name, reference, price, tva, age, description, stock, stock_min, fk_wine_family, fk_supplier) " +
            "VALUES (@name, @reference, @price, @tva, @age, @description, @stock, @stock_min, @fk_wine_family, @fk_supplier)";

        MySqlCommand command = new(sql, DBConnection.Connection);

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

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM `product` " +
            $"WHERE `pk_product` = {id}";

        DBConnection.Connection.Open();
        new MySqlCommand(sql, DBConnection.Connection).ExecuteNonQuery();
        DBConnection.Connection.Close();
    }

    public static void Update(Product product)
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
            $"WHERE `pk_product` =  {product.PkProduct}`";

        MySqlCommand command = new(sql, DBConnection.Connection);

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

        DBConnection.Connection.Open();
        command.ExecuteNonQuery();
        DBConnection.Connection.Close();
    }
}
