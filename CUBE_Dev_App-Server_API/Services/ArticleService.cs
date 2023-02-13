using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace CUBE_Dev_App_Server_API.Services;

public static class ArticleService
{
    #region Services

    public static bool GetAll(out List<Article> articles)
    {
        string sql = @"SELECT a.IDArticle, a.Reference, a.Name, a.WineYear, a.BoxMinQuantity, a.BoxBuyingPrice,
                       a.UnitPriceTTC, a.BoxPriceTTC, a.TVA, a.BoxOptimalQuantity, a.BoxVirtualQuantity,
                       a.UnitVirtualQuantity, a.BoxStockQuantity, a.UnitStockQuantity, a.BottleQuantityPerBox,
                       a.Description, a.ImageLink, a.IDSupplier, s.Name AS SupplierName, s.Address, s.PostalCode,
                       s.Town, s.Country, s.Email, a.idWineFamily, wf.Name AS WineFamilyName
                       FROM article a
                       LEFT JOIN supplier s ON a.IDSupplier = s.IDSupplier
                       LEFT JOIN winefamily wf ON a.idWineFamily = wf.idWineFamily";


        List<Article>? result = ExecuteGetAllRequest(sql);

        if (result == null)
        {
            articles = new List<Article>();
            return false;
        }
        else
        {
            articles = result;
            return true;
        }
    }

    public static bool GetAllSameFamily(int idWineFamily, out List<Article> articles)
    {
        string sql = $@"SELECT a.IDArticle, a.Reference, a.Name, a.WineYear, a.BoxMinQuantity, a.BoxBuyingPrice,
                       a.UnitPriceTTC, a.BoxPriceTTC, a.TVA, a.BoxOptimalQuantity, a.BoxVirtualQuantity,
                       a.UnitVirtualQuantity, a.BoxStockQuantity, a.UnitStockQuantity, a.BottleQuantityPerBox,
                       a.Description, a.ImageLink, a.IDSupplier, s.Name AS SupplierName, s.Address, s.PostalCode,
                       s.Town, s.Country, s.Email, a.idWineFamily, wf.Name AS WineFamilyName
                       FROM article a
                       LEFT JOIN supplier s ON a.IDSupplier = s.IDSupplier
                       LEFT JOIN winefamily wf ON a.idWineFamily = wf.idWineFamily
                       WHERE a.idWineFamily = {idWineFamily}";


        List<Article>? result = ExecuteGetAllRequest(sql);

        if (result == null)
        {
            articles = new List<Article>();
            return false;
        }
        else
        {
            articles = result;
            return true;
        }
    }

    public static bool GetAllSameSupplier(int idSupplier, out List<Article> articles)
    {
        string sql = $@"SELECT a.IDArticle, a.Reference, a.Name, a.WineYear, a.BoxMinQuantity, a.BoxBuyingPrice,
                       a.UnitPriceTTC, a.BoxPriceTTC, a.TVA, a.BoxOptimalQuantity, a.BoxVirtualQuantity,
                       a.UnitVirtualQuantity, a.BoxStockQuantity, a.UnitStockQuantity, a.BottleQuantityPerBox,
                       a.Description, a.ImageLink, a.IDSupplier, s.Name AS SupplierName, s.Address, s.PostalCode,
                       s.Town, s.Country, s.Email, a.idWineFamily, wf.Name AS WineFamilyName
                       FROM article a
                       LEFT JOIN supplier s ON a.IDSupplier = s.IDSupplier
                       LEFT JOIN winefamily wf ON a.idWineFamily = wf.idWineFamily
                       WHERE a.IDSupplier = {idSupplier}";


        List<Article>? result = ExecuteGetAllRequest(sql);

        if (result == null)
        {
            articles = new List<Article>();
            return false;
        }
        else
        {
            articles = result;
            return true;
        }
    }

    public static bool Get(int id, out Article? article)
    {
        string sql = $@"SELECT a.IDArticle, a.Reference, a.Name, a.WineYear, a.BoxMinQuantity, a.BoxBuyingPrice,
                       a.UnitPriceTTC, a.BoxPriceTTC, a.TVA, a.BoxOptimalQuantity, a.BoxVirtualQuantity,
                       a.UnitVirtualQuantity, a.BoxStockQuantity, a.UnitStockQuantity, a.BottleQuantityPerBox,
                       a.Description, a.ImageLink, a.IDSupplier, s.Name AS SupplierName, s.Address, s.PostalCode,
                       s.Town, s.Country, s.Email, a.idWineFamily, wf.Name AS WineFamilyName
                       FROM article a
                       LEFT JOIN supplier s ON a.IDSupplier = s.IDSupplier
                       LEFT JOIN winefamily wf ON a.idWineFamily = wf.idWineFamily
                       WHERE a.IDArticle = {id};";

        article = new Article();

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
            return false;

        while (reader.Read())
        {
            article = new Article
            {
                IDArticle = reader.GetInt32("IDArticle"),
                Reference = reader.IsDBNull("Reference") ? null : reader.GetString("Reference"),
                Name = reader.GetString("Name"),
                WineYear = reader.GetInt32("WineYear"),
                BoxMinQuantity = reader.GetInt32("BoxMinQuantity"),
                BoxBuyingPrice = reader.GetDecimal("BoxBuyingPrice"),
                UnitPriceTTC = reader.GetDecimal("UnitPriceTTC"),
                BoxPriceTTC = reader.GetDecimal("BoxPriceTTC"),
                TVA = reader.GetDecimal("TVA"),
                BoxOptimalQuantity = reader.GetInt32("BoxOptimalQuantity"),
                BoxStockQuantity = reader.GetInt32("BoxStockQuantity"),
                UnitStockQuantity = reader.GetInt32("UnitStockQuantity"),
                BoxVirtualQuantity = reader.GetInt32("BoxVirtualQuantity"),
                UnitVirtualQuantity = reader.GetInt32("UnitVirtualQuantity"),
                BottleQuantityPerBox = reader.GetInt32("BottleQuantityPerBox"),
                Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                ImageLink = reader.IsDBNull("ImageLink") ? null : reader.GetString("ImageLink"),
                IDSupplier = reader.GetInt32("IDSupplier"),
                Supplier = new Supplier
                {
                    IDSupplier = reader.GetInt32("IDSupplier"),
                    Name = reader.GetString("SupplierName"),
                    Address = reader.GetString("Address"),
                    PostalCode = reader.GetString("PostalCode"),
                    Town = reader.GetString("Town"),
                    Country = reader.GetString("Country"),
                    Email = reader.GetString("Email")
                },
                idWineFamily = reader.GetInt32("idWineFamily"),
                WineFamily = new WineFamily
                {
                    idWineFamily = reader.GetInt32("idWineFamily"),
                    Name = reader.GetString("WineFamilyName")
                }
            };
        }
        return true;

    }

    public static bool Add(Article article)
    {
        if (!SupplierService.Get(article.IDSupplier, out _))
        {
            return false;
        }

        if (!WineFamilyService.Get(article.idWineFamily, out _))
        {
            return false;
        }

        article.BoxVirtualQuantity = article.BoxStockQuantity;
        article.UnitVirtualQuantity = article.UnitStockQuantity;

        string sql = "INSERT INTO Article " +
            "(Reference, Name, WineYear, BoxMinQuantity, BoxBuyingPrice, UnitPriceTTC, BoxPriceTTC, TVA, BoxOptimalQuantity, BoxVirtualQuantity, UnitVirtualQuantity, BoxStockQuantity, UnitStockQuantity, BottleQuantityPerBox, Description, ImageLink, IDSupplier, idWineFamily)" +
            "VALUES (@Reference, @Name, @WineYear, @BoxMinQuantity, @BoxBuyingPrice, @UnitPriceTTC, @BoxPriceTTC, @TVA, @BoxOptimalQuantity, @BoxVirtualQuantity, @UnitVirtualQuantity, @BoxStockQuantity, @UnitStockQuantity, @BottleQuantityPerBox, @Description, @ImageLink, @IDSupplier, @idWineFamily)";

        using MySqlCommand command = new MySqlCommand(sql);

        command.Parameters.AddWithValue("@Reference", article.Reference);
        command.Parameters.AddWithValue("@Name", article.Name);
        command.Parameters.AddWithValue("@WineYear", article.WineYear);
        command.Parameters.AddWithValue("@BoxMinQuantity", article.BoxMinQuantity);
        command.Parameters.AddWithValue("@BoxBuyingPrice", article.BoxBuyingPrice);
        command.Parameters.AddWithValue("@UnitPriceTTC", article.UnitPriceTTC);
        command.Parameters.AddWithValue("@BoxPriceTTC", article.BoxPriceTTC);
        command.Parameters.AddWithValue("@TVA", article.TVA);
        command.Parameters.AddWithValue("@BoxOptimalQuantity", article.BoxOptimalQuantity);
        command.Parameters.AddWithValue("@BoxVirtualQuantity", article.BoxVirtualQuantity);
        command.Parameters.AddWithValue("@UnitVirtualQuantity", article.UnitVirtualQuantity);
        command.Parameters.AddWithValue("@BoxStockQuantity", article.BoxStockQuantity);
        command.Parameters.AddWithValue("@UnitStockQuantity", article.UnitStockQuantity);
        command.Parameters.AddWithValue("@BottleQuantityPerBox", article.BottleQuantityPerBox);
        command.Parameters.AddWithValue("@Description", article.Description);
        command.Parameters.AddWithValue("@ImageLink", article.ImageLink);
        command.Parameters.AddWithValue("@IDSupplier", article.IDSupplier);
        command.Parameters.AddWithValue("@idWineFamily", article.idWineFamily);

        return DBConnection.ExecuteCommandToDB(command);
    }

    public static bool Update(Article article)
    {
        if (!SupplierService.Get(article.IDSupplier, out _))
        {
            return false;
        }

        if (!WineFamilyService.Get(article.idWineFamily, out _))
        {
            return false;
        }

        string sql = @"UPDATE Article SET 
                            Reference = @Reference, 
                            Name = @Name, 
                            WineYear = @WineYear, 
                            BoxMinQuantity = @BoxMinQuantity, 
                            BoxBuyingPrice = @BoxBuyingPrice, 
                            UnitPriceTTC = @UnitPriceTTC, 
                            BoxPriceTTC = @BoxPriceTTC, 
                            TVA = @TVA, 
                            BoxOptimalQuantity = @BoxOptimalQuantity, 
                            BoxVirtualQuantity = @BoxVirtualQuantity, 
                            UnitVirtualQuantity = @UnitVirtualQuantity, 
                            BoxStockQuantity = @BoxStockQuantity, 
                            UnitStockQuantity = @UnitStockQuantity, 
                            BottleQuantityPerBox = @BottleQuantityPerBox, 
                            Description = @Description, 
                            ImageLink = @ImageLink, 
                            IDSupplier = @IDSupplier, 
                            idWineFamily = @idWineFamily 
                            WHERE IDArticle = @IDArticle";

        using MySqlCommand command = new MySqlCommand(sql);

        command.Parameters.AddWithValue("@Reference", article.Reference);
        command.Parameters.AddWithValue("@Name", article.Name);
        command.Parameters.AddWithValue("@WineYear", article.WineYear);
        command.Parameters.AddWithValue("@BoxMinQuantity", article.BoxMinQuantity);
        command.Parameters.AddWithValue("@BoxBuyingPrice", article.BoxBuyingPrice);
        command.Parameters.AddWithValue("@UnitPriceTTC", article.UnitPriceTTC);
        command.Parameters.AddWithValue("@BoxPriceTTC", article.BoxPriceTTC);
        command.Parameters.AddWithValue("@TVA", article.TVA);
        command.Parameters.AddWithValue("@BoxOptimalQuantity", article.BoxOptimalQuantity);
        command.Parameters.AddWithValue("@BoxVirtualQuantity", article.BoxVirtualQuantity);
        command.Parameters.AddWithValue("@UnitVirtualQuantity", article.UnitVirtualQuantity);
        command.Parameters.AddWithValue("@BoxStockQuantity", article.BoxStockQuantity);
        command.Parameters.AddWithValue("@UnitStockQuantity", article.UnitStockQuantity);
        command.Parameters.AddWithValue("@BottleQuantityPerBox", article.BottleQuantityPerBox);
        command.Parameters.AddWithValue("@Description", article.Description);
        command.Parameters.AddWithValue("@ImageLink", article.ImageLink);
        command.Parameters.AddWithValue("@IDSupplier", article.IDSupplier);
        command.Parameters.AddWithValue("@idWineFamily", article.idWineFamily);
        command.Parameters.AddWithValue("@IDArticle", article.IDArticle);

        return DBConnection.ExecuteCommandToDB(command);
    }

    public static bool Delete(int id)
    {
        string sql = $"DELETE FROM Article WHERE IDArticle = {id}";

        using MySqlCommand command = new MySqlCommand(sql);

        return DBConnection.ExecuteCommandToDB(command);
    }

    #endregion

    #region Private Methods

    private static List<Article>? ExecuteGetAllRequest(string sqlQuery)
    {
        using MySqlDataReader? reader = DBConnection.ExecuteReader(sqlQuery);

        if (reader == null)
            return null;

        List<Article> articles = new List<Article>();

        while (reader.Read())
        {
            articles.Add(new Article
            {
                IDArticle = reader.GetInt32("IDArticle"),
                Reference = reader.IsDBNull("Reference") ? null : reader.GetString("Reference"),
                Name = reader.GetString("Name"),
                WineYear = reader.GetInt32("WineYear"),
                BoxMinQuantity = reader.GetInt32("BoxMinQuantity"),
                BoxBuyingPrice = reader.GetDecimal("BoxBuyingPrice"),
                UnitPriceTTC = reader.GetDecimal("UnitPriceTTC"),
                BoxPriceTTC = reader.GetDecimal("BoxPriceTTC"),
                TVA = reader.GetDecimal("TVA"),
                BoxOptimalQuantity = reader.GetInt32("BoxOptimalQuantity"),
                BoxStockQuantity = reader.GetInt32("BoxStockQuantity"),
                UnitStockQuantity = reader.GetInt32("UnitStockQuantity"),
                BoxVirtualQuantity = reader.GetInt32("BoxVirtualQuantity"),
                UnitVirtualQuantity = reader.GetInt32("UnitVirtualQuantity"),
                BottleQuantityPerBox = reader.GetInt32("BottleQuantityPerBox"),
                Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                ImageLink = reader.IsDBNull("ImageLink") ? null : reader.GetString("ImageLink"),
                IDSupplier = reader.GetInt32("IDSupplier"),
                Supplier = new Supplier
                {
                    IDSupplier = reader.GetInt32("IDSupplier"),
                    Name = reader.GetString("SupplierName"),
                    Address = reader.GetString("Address"),
                    PostalCode = reader.GetString("PostalCode"),
                    Town = reader.GetString("Town"),
                    Country = reader.GetString("Country"),
                    Email = reader.GetString("Email")
                },
                idWineFamily = reader.GetInt32("idWineFamily"),
                WineFamily = new WineFamily
                {
                    idWineFamily = reader.GetInt32("idWineFamily"),
                    Name = reader.GetString("WineFamilyName")
                }
            });
        }

        return articles;
    }

    #endregion
}
