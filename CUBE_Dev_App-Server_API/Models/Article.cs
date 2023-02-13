namespace CUBE_Dev_App_Server_API.Models;

public class Article
{
    public int IDArticle { get; set; }
    public string? Reference { get; set; }
    public string Name { get; set; } = string.Empty;
    public int WineYear { get; set; }
    public int BoxMinQuantity { get; set; }
    public decimal BoxBuyingPrice { get; set; }
    public decimal UnitPriceTTC { get; set; }
    public decimal BoxPriceTTC { get; set; }
    public decimal TVA { get; set; }
    public int BoxOptimalQuantity { get; set; }
    public int BoxVirtualQuantity { get; set; }
    public int UnitVirtualQuantity { get; set; }
    public int BoxStockQuantity { get; set; }
    public int UnitStockQuantity { get; set; }
    public int BottleQuantityPerBox { get; set; }
    public string? Description { get; set; }
    public string? ImageLink { get; set; }
    public int IDSupplier { get; set; }
    public Supplier Supplier { get; set; } = new();
    public int idWineFamily { get; set; }
    public WineFamily WineFamily { get; set; } = new();
}
