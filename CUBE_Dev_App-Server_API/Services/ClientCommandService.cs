using CUBE_Dev_App_Server_API.Models;
using MySql.Data.MySqlClient;

namespace CUBE_Dev_App_Server_API.Services;

public static class ClientCommandService
{

    public static bool GetAllHeaders(out List<ClientCommand> clientCommands)
    {
        string sql = @"SELECT cc.*, c.*, cs.*
                    FROM ClientCommand cc
                    INNER JOIN Client c ON cc.IDClient = c.IDClient
                    INNER JOIN CommandStatus cs ON cc.IDCommandStatus = cs.IDCommandStatus";

        clientCommands = new List<ClientCommand>();

        using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

        if (reader == null)
            return false;

        while (reader.Read())
        {
            clientCommands.Add(new ClientCommand
            {
                IDClientCommand = reader.GetInt32("IDClientCommand"),
                CommandDate = reader.GetDateTime("CommandDate"),
                TotalCostTTC = reader.GetDecimal("TotalCostTTC"),
                TotalCostHT = reader.GetDecimal("TotalCostHT"),
                IDClient = reader.GetInt32("IDClient"),
                IDCommandStatus = reader.GetInt32("IDCommandStatus"),

                Client = new Client_DTO
                {
                    IDClient = reader.GetInt32("IDClient"),
                    Firstname = reader.GetString("Firstname"),
                    Lastname = reader.GetString("Lastname"),
                    Address = reader.GetString("Address"),
                    PostalCode = reader.GetString("PostalCode"),
                    Town = reader.GetString("Town"),
                    Country = reader.GetString("Country"),
                    Email = reader.GetString("Email")
                },

                CommandStatus = new CommandStatus
                {
                    IDCommandStatus = reader.GetInt32("IDCommandStatus"),
                    Name = reader.GetString("Name")
                }
            });
        }

        return true;
    }

    public static bool Get(int id, out ClientCommand? clientCommand)
    {
        // Select the header of the command

        string sql = $@"SELECT * FROM ClientCommand
                    LEFT JOIN Client ON ClientCommand.IDClient = Client.IDClient
                    LEFT JOIN CommandStatus ON ClientCommand.IDCommandStatus = CommandStatus.IDCommandStatus
                    WHERE IDClientCommand = {id}";

        using (MySqlDataReader? reader = DBConnection.ExecuteReader(sql))
        {
            if (reader == null)
            {
                clientCommand = null;
                return false;
            }

            clientCommand = new ClientCommand();

            if (reader.Read())
            {
                clientCommand = new ClientCommand
                {
                    IDClientCommand = reader.GetInt32("IDClientCommand"),
                    CommandDate = reader.GetDateTime("CommandDate"),
                    TotalCostTTC = reader.GetDecimal("TotalCostTTC"),
                    TotalCostHT = reader.GetDecimal("TotalCostHT"),
                    IDClient = reader.GetInt32("IDClient"),
                    IDCommandStatus = reader.GetInt32("IDCommandStatus"),
                    Client = new Client_DTO
                    {
                        IDClient = reader.GetInt32("IDClient"),
                        Firstname = reader.GetString("Firstname"),
                        Lastname = reader.GetString("Lastname"),
                        Email = reader.GetString("Email"),
                        Address = reader.GetString("Address"),
                        Town = reader.GetString("Town"),
                        PostalCode = reader.GetString("PostalCode"),
                        Country = reader.GetString("Country")
                    },
                    CommandStatus = new CommandStatus
                    {
                        IDCommandStatus = reader.GetInt32("IDCommandStatus"),
                        Name = reader.GetString("Name")
                    },
                    ClientCommandList = new List<ClientCommandList>()
                };
            }
        }

        // Select the rows of the command

        sql = $@"SELECT * FROM ClientCommandList
                LEFT JOIN QuantityType ON ClientCommandList.IDQuantityType = QuantityType.IDQuantityType
                WHERE IDClientCommand = {id}";

        using (MySqlDataReader? reader = DBConnection.ExecuteReader(sql))
        {
            if (reader == null)
            {
                clientCommand = null;
                return false;
            }

            while (reader.Read())
            {
                ClientCommandList clientCommandList = new ClientCommandList
                {
                    IDArticle = reader.GetInt32("IDArticle"),
                    Quantity = reader.GetInt32("Quantity"),
                    IDQuantityType = reader.GetInt32("IDQuantityType"),
                    QuantityType = new QuantityType()
                    {
                        IDQuantityType = reader.GetInt32("IDQuantityType"),
                        Name = reader.GetString("Name")
                    }
                };

                clientCommand.ClientCommandList.Add(clientCommandList);
            }
        }

        // Add the articles data with another reader

        foreach (ClientCommandList clientCommandList in clientCommand.ClientCommandList)
        {
            if (ArticleService.Get(clientCommandList.IDArticle, out Article? article)
                    && article != null)
            {
                clientCommandList.Article = article;
            }
            else
            {
                clientCommand = null;
                return false;
            }
        }

        return true;
    }

    public static bool Add(ClientCommand clientCommand)
    {
        // Calculate total cost

        decimal totalCostTTC = 0;
        decimal totalCostHT = 0;

        foreach (ClientCommandList clientCommandArticle in clientCommand.ClientCommandList)
        {
            if (!ArticleService.Get(clientCommandArticle.IDArticle, out Article? article))
                return false;

            if (article == null)
                return false;

            if (clientCommandArticle.IDQuantityType == 1)
            {
                totalCostTTC += article.UnitPriceTTC;
                totalCostHT += article.UnitPriceTTC / (1 + (article.TVA / 100));
            }
            else
            {
                totalCostTTC += article.BoxPriceTTC;
                totalCostHT += article.BoxPriceTTC / (1 + (article.TVA / 100));
            }
        }

        clientCommand.TotalCostTTC = totalCostTTC;
        clientCommand.TotalCostHT = totalCostHT;

        // Insert the header of the command
        string sql = "INSERT INTO ClientCommand " +
            "(CommandDate, TotalCostTTC, TotalCostHT, IDClient, IDCommandStatus)" +
            "VALUES (@CommandDate, @TotalCostTTC, @TotalCostHT, @IDClient, @IDCommandStatus)";

        using (MySqlCommand command = new MySqlCommand(sql))
        {
            command.Parameters.AddWithValue("@CommandDate", clientCommand.CommandDate);
            command.Parameters.AddWithValue("@TotalCostTTC", clientCommand.TotalCostTTC);
            command.Parameters.AddWithValue("@TotalCostHT", clientCommand.TotalCostHT);
            command.Parameters.AddWithValue("@IDClient", clientCommand.IDClient);
            command.Parameters.AddWithValue("@IDCommandStatus", clientCommand.IDCommandStatus);

            if (!DBConnection.ExecuteCommandToDB(command))
            {
                return false;
            }
        }

        int highestPrimaryKey = DBConnection.GetHighestPrimaryKey("ClientCommand");

        // Insert the rows of the command
        sql = "INSERT INTO ClientCommandList (IDArticle, IDClientCommand, Quantity, IDQuantityType) VALUES ";

        List<string> listArticles = new List<string>();

        foreach (ClientCommandList article in clientCommand.ClientCommandList)
        {
            listArticles.Add($"({article.IDArticle}, {highestPrimaryKey}, {article.Quantity}, {article.IDQuantityType})");
        }

        sql += string.Join(", ", listArticles);

        using (MySqlCommand command = new MySqlCommand(sql))
        {
            if (!DBConnection.ExecuteCommandToDB(command))
            {
                return false;
            }
        }

        if (!Get(highestPrimaryKey, out ClientCommand? createdClientCommand))
            return false;

        if (createdClientCommand == null)
            return false;

        ControlStock(createdClientCommand.ClientCommandList);

        return true;
    }

    private static bool ControlStock(List<ClientCommandList> clientCommandList)
    {
        foreach (ClientCommandList commandListItem in clientCommandList)
        {
            Article commandArticle = commandListItem.Article;

            int commandBoxQuantity;
            int boxVirtualQuantityOrigin = commandArticle.BoxVirtualQuantity;

            // Controls if the type of quantity is by boxes
            if (commandListItem.IDQuantityType == 2)
            {
                commandBoxQuantity = commandListItem.Quantity;
            }
            else
            {
                // Simulate the opening of new boxes to get the required amount of unique bottles

                int boxesOpened = 0;
                int unitVirtualQuantity = commandArticle.UnitVirtualQuantity;

                while (commandListItem.Quantity > unitVirtualQuantity)
                {
                    unitVirtualQuantity += commandArticle.BottleQuantityPerBox;
                    boxesOpened++;
                }

                 commandArticle.UnitVirtualQuantity = unitVirtualQuantity - commandListItem.Quantity;
                commandBoxQuantity = boxesOpened;
            }

            commandArticle.BoxVirtualQuantity -= commandBoxQuantity;

            // Update virtual quantities of article
            if (!ArticleService.Update(commandArticle))
                return false;

            int boxMinQuantity = commandListItem.Article.BoxMinQuantity;
            int boxOptiQuantity = commandListItem.Article.BoxOptimalQuantity;

            // Creates a new command if there are not enough boxes
            if (commandBoxQuantity >= boxVirtualQuantityOrigin - boxMinQuantity)
            {
                int quantityToCommand = commandBoxQuantity + boxOptiQuantity - boxVirtualQuantityOrigin;
                if (!CreateSupplierCommandAuto(commandArticle.IDSupplier, commandArticle.IDArticle, quantityToCommand))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool CreateSupplierCommandAuto(int idSupplier, int idArticle, int quantity)
    {
        SupplierCommand supplierCommand = new SupplierCommand()
        {
            CommandDate = DateTime.Now,
            TransportCost = 0,
            IDCommandStatus = 2,
            IDCommandType = 2,
            IDSupplier = idSupplier,
            SupplierCommandList = new List<SupplierCommandList>()
            {
                new SupplierCommandList()
                {
                    IDArticle = idArticle,
                    Quantity = quantity
                }
            }
        };

        if (!SupplierCommandService.Add(supplierCommand))
        {
            return false;
        }

        return true;
    }

    public static bool Close(ClientCommand clientCommand)
    {
        // Add the articles in stock
        foreach (ClientCommandList commandList in clientCommand.ClientCommandList)
        {
            string sql;
            if (commandList.IDQuantityType == 1)
            {
                sql = $@"UPDATE Article
            SET UnitStockQuantity = UnitStockQuantity - {commandList.Quantity}
            WHERE IDArticle = {commandList.IDArticle}";
            }
            else
            {
                sql = $@"UPDATE Article
            SET BoxStockQuantity = BoxStockQuantity - {commandList.Quantity}
            WHERE IDArticle = {commandList.IDArticle}";
            }

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                if (!DBConnection.ExecuteCommandToDB(command))
                {
                    return false;
                }
            }
        }

        return SetCommandToClosed(clientCommand.IDClientCommand);

    }

    public static bool Cancel(ClientCommand supplierCommand)
    {
        // Remove the articles from the virtual counters
        foreach (ClientCommandList commandList in supplierCommand.ClientCommandList)
        {
            string sql;

            if (commandList.IDQuantityType == 1)
            {
                int boxQuantity = 0;
                int unitQuantity = commandList.Quantity;

                while (commandList.Quantity > commandList.Article.BottleQuantityPerBox)
                {
                    unitQuantity -= commandList.Article.BottleQuantityPerBox;
                    boxQuantity++;
                }

                sql = $@"UPDATE Article
            SET UnitVirtualQuantity = UnitVirtualQuantity + {unitQuantity},
            SET BoxVirtualQuantity = BoxVirtualQuantity + {boxQuantity}
            WHERE IDArticle = {commandList.IDArticle}";
            }
            else
            {
                sql = $@"UPDATE Article
            SET BoxVirtualQuantity = BoxVirtualQuantity + {commandList.Quantity}
            WHERE IDArticle = {commandList.IDArticle}";
            }

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                if (!DBConnection.ExecuteCommandToDB(command))
                {
                    return false;
                }
            }
        }

        return SetCommandToClosed(supplierCommand.IDClientCommand);

    }

    public static bool SetCommandToClosed(int id)
    {
        // Set command to closed

        string sql = $@"UPDATE ClientCommand
                SET IDCommandStatus = 1
                WHERE IDClientCommand = {id}";

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
