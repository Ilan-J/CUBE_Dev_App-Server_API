using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class SupplierCommandService
{
    public static bool GetAllHeaders(out List<SupplierCommand> supplierCommands)
    {
        string sql = $@"SELECT SupplierCommand.*, Supplier.Name as SupplierName, Supplier.Email, Supplier.Address, Supplier.Town, Supplier.PostalCode, Supplier.Country,
                        CommandStatus.Name as CommandStatusName, CommandType.Name as CommandTypeName
                FROM SupplierCommand
                LEFT JOIN Supplier ON SupplierCommand.IDSupplier = Supplier.IDSupplier
                LEFT JOIN CommandStatus ON SupplierCommand.IDCommandStatus = CommandStatus.IDCommandStatus
                LEFT JOIN CommandType ON SupplierCommand.IDCommandType = CommandType.IDCommandType";

        supplierCommands = new List<SupplierCommand>();

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
            return false;

        while (reader.Read())
        {
            supplierCommands.Add(new SupplierCommand
            {
                IDSupplierCommand = reader.GetInt32("IDSupplierCommand"),
                TransportCost = reader.GetDecimal("TransportCost"),
                CommandDate = reader.GetDateTime("CommandDate"),
                TotalCost = reader.GetDecimal("TotalCost"),
                IDSupplier = reader.GetInt32("IDSupplier"),
                IDCommandStatus = reader.GetInt32("IDCommandStatus"),
                IDCommandType = reader.GetInt32("IDCommandType"),

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

                CommandStatus = new CommandStatus
                {
                    IDCommandStatus = reader.GetInt32("IDCommandStatus"),
                    Name = reader.GetString("CommandStatusName")
                },

                CommandType = new CommandType
                {
                    IDCommandType = reader.GetInt32("IDCommandType"),
                    Name = reader.GetString("CommandTypeName")
                }
            });
        }

        return true;
    }

    public static bool Get(int id, out SupplierCommand? supplierCommand)
    {
        // Select the header of the command

        string sql = $@"SELECT SupplierCommand.*, Supplier.Name as SupplierName, Supplier.Email, Supplier.Address, Supplier.Town, Supplier.PostalCode, Supplier.Country,
                        CommandStatus.Name as CommandStatusName, CommandType.Name as CommandTypeName
                FROM SupplierCommand
                LEFT JOIN Supplier ON SupplierCommand.IDSupplier = Supplier.IDSupplier
                LEFT JOIN CommandStatus ON SupplierCommand.IDCommandStatus = CommandStatus.IDCommandStatus
                LEFT JOIN CommandType ON SupplierCommand.IDCommandType = CommandType.IDCommandType
                WHERE IDSupplierCommand = {id}";

        using (MySqlDataReader? reader = DBConnection.ExecuteReader(sql))
        {
            if (reader == null)
            {
                supplierCommand = null;
                return false;
            }

            supplierCommand = new SupplierCommand();

            if (reader.Read())
            {
                supplierCommand = new SupplierCommand
                {
                    IDSupplierCommand = reader.GetInt32("IDSupplierCommand"),
                    TransportCost = reader.GetDecimal("TransportCost"),
                    CommandDate = reader.GetDateTime("CommandDate"),
                    TotalCost = reader.GetDecimal("TotalCost"),
                    IDSupplier = reader.GetInt32("IDSupplier"),
                    IDCommandStatus = reader.GetInt32("IDCommandStatus"),
                    IDCommandType = reader.GetInt32("IDCommandType"),
                    Supplier = new Supplier
                    {
                        IDSupplier = reader.GetInt32("IDSupplier"),
                        Name = reader.GetString("SupplierName"),
                        Email = reader.GetString("Email"),
                        Address = reader.GetString("Address"),
                        Town = reader.GetString("Town"),
                        PostalCode = reader.GetString("PostalCode"),
                        Country = reader.GetString("Country")
                    },
                    CommandStatus = new CommandStatus
                    {
                        IDCommandStatus = reader.GetInt32("IDCommandStatus"),
                        Name = reader.GetString("CommandStatusName")
                    },
                    CommandType = new CommandType
                    {
                        IDCommandType = reader.GetInt32("IDCommandType"),
                        Name = reader.GetString("CommandTypeName")
                    },
                    SupplierCommandList = new List<SupplierCommandList>()
                };
            }

            reader.Close();
        }

        // Select the rows of the command

        sql = $@"SELECT * FROM SupplierCommandList
            WHERE IDSupplierCommand = {id}";

        using (MySqlDataReader? reader = DBConnection.ExecuteReader(sql))
        {
            if (reader == null)
            {
                supplierCommand = null;
                return false;
            }

            while (reader.Read())
            {
                SupplierCommandList supplierCommandList = new SupplierCommandList
                {
                    IDArticle = reader.GetInt32("IDArticle"),
                    Quantity = reader.GetInt32("Quantity")
                };

                supplierCommand.SupplierCommandList.Add(supplierCommandList);
            }
        }

        // Add the articles data with another reader

        foreach (SupplierCommandList supplierCommandList in supplierCommand.SupplierCommandList)
        {
            if (ArticleService.Get(supplierCommandList.IDArticle, out Article? article)
                    && article != null)
            {
                supplierCommandList.Article = article;
            }
            else
            {
                supplierCommand = null;
                return false;
            }
        }

        return true;
    }

    public static bool Add(SupplierCommand supplierCommand)
    {
        // Calculate total cost
        decimal totalCost = 0;
        foreach (SupplierCommandList supplierCommandArticle in supplierCommand.SupplierCommandList)
        {
            if (!ArticleService.Get(supplierCommandArticle.IDArticle, out Article? article))
                return false;

            if (article == null)
                return false;

            supplierCommandArticle.Article = article;

            totalCost += supplierCommandArticle.Quantity * article.BoxBuyingPrice;
        }

        supplierCommand.TotalCost = totalCost;

        // Insert the header of the command

        string sql = "INSERT INTO SupplierCommand " +
            "(TransportCost, CommandDate, TotalCost, IDSupplier, IDCommandStatus, IDCommandType)" +
            "VALUES (@TransportCost, @CommandDate, @TotalCost, @IDSupplier, @IDCommandStatus, @IDCommandType)";

        using (MySqlCommand command = new MySqlCommand(sql))
        {
            command.Parameters.AddWithValue("@TransportCost", supplierCommand.TransportCost);
            command.Parameters.AddWithValue("@CommandDate", supplierCommand.CommandDate);
            command.Parameters.AddWithValue("@TotalCost", supplierCommand.TotalCost);
            command.Parameters.AddWithValue("@IDSupplier", supplierCommand.IDSupplier);
            command.Parameters.AddWithValue("@IDCommandStatus", supplierCommand.IDCommandStatus);
            command.Parameters.AddWithValue("@IDCommandType", supplierCommand.IDCommandType);

            if (!DBConnection.ExecuteCommandToDB(command))
            {
                return false;
            }
        }

        int highestPrimaryKey = DBConnection.GetHighestPrimaryKey("SupplierCommand");

        // Insert the rows of the command

        sql = "INSERT INTO SupplierCommandList (IDArticle, IDSupplierCommand, Quantity) VALUES ";

        List<string> listArticles = new List<string>();

        foreach (SupplierCommandList commandList in supplierCommand.SupplierCommandList)
        {
            listArticles.Add($"({commandList.IDArticle}, {highestPrimaryKey}, {commandList.Quantity})");
        }

        sql += string.Join(", ", listArticles);

        using (MySqlCommand command = new MySqlCommand(sql))
        {
            if (!DBConnection.ExecuteCommandToDB(command))
            {
                return false;
            }
        }

        // Update the box virtual quantities of each article
        foreach (SupplierCommandList commandList in supplierCommand.SupplierCommandList)
        {
            Article article = commandList.Article;

            article.BoxVirtualQuantity += commandList.Quantity;

            if (!ArticleService.Update(article))
            {
                return false;
            }
        }

        return true;
    }

    public static bool Close(SupplierCommand supplierCommand)
    {
        // Add the articles in stock
        foreach (SupplierCommandList commandList in supplierCommand.SupplierCommandList)
        {
            string sql = $@"UPDATE Article
            SET BoxStockQuantity = BoxStockQuantity + {commandList.Quantity}
            WHERE IDArticle = {commandList.IDArticle}";

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                if (!DBConnection.ExecuteCommandToDB(command))
                {
                    return false;
                }
            }
        }

        return SetCommandToClosed(supplierCommand.IDSupplierCommand);

    }

    public static bool Cancel(SupplierCommand supplierCommand)
    {
        // Remove the articles from the virtual counters
        foreach (SupplierCommandList commandList in supplierCommand.SupplierCommandList)
        {
            string sql = $@"UPDATE Article
            SET BoxVirtualQuantity = BoxVirtualQuantity - {commandList.Quantity}
            WHERE IDArticle = {commandList.IDArticle}";

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                if (!DBConnection.ExecuteCommandToDB(command))
                {
                    return false;
                }
            }
        }

        return SetCommandToClosed(supplierCommand.IDSupplierCommand);

    }

    public static bool SetCommandToClosed(int id)
    {
        // Set command to closed

        string sql = $@"UPDATE SupplierCommand
                SET IDCommandStatus = 1
                WHERE IDSupplierCommand = {id}";

        using (MySqlCommand command = new MySqlCommand(sql))
        {
            if (!DBConnection.ExecuteCommandToDB(command))
            {
                return false;
            }
        }

        return true;
    }
}
