namespace CUBE_Dev_App_Server_API.Models;

public class Article
{
    public int IDArticle { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int WineYear { get; set; }
    public int Quantity { get; set; }
    public int MinQuantity { get; set; }
    public float BuyingPrice { get; set; }
    public float PriceTTC { get; set; }
    public int TVA { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageLink { get; set; } = string.Empty;
    public WineFamily WineFamily { get; set; } = new();
    public Supplier Supplier { get; set; } = new();
}
