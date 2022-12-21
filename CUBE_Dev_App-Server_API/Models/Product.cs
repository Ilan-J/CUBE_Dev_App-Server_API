namespace CUBE_Dev_App_Server_API.Models;

public class Product
{
    public int          PkProduct { get; set; }

    public string       Name { get; set; }          = string.Empty;
    public string       Reference { get; set; }     = string.Empty;

    public int          Price { get; set; }
    public int          TVA { get; set; }

    public int          Age { get; set; }
    public string       Description { get; set; }   = string.Empty;

    public int          Stock { get; set; }
    public int          StockMin { get; set; }

    public int          FkWineFamily { get; set; }
    public WineFamily?  WineFamily { get; set; }

    public int          FkSupplier { get; set; }
    public Supplier?    Supplier { get; set; }
}
