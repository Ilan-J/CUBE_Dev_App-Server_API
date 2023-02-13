namespace CUBE_Dev_App_Server_API.Models;

public class ClientCommandList
{
    public int IDArticle { get; set; }
    public int Quantity { get; set; }
    public int IDQuantityType { get; set; }

    public Article Article { get; set; } = new();
    public QuantityType QuantityType { get; set; } = new();
}