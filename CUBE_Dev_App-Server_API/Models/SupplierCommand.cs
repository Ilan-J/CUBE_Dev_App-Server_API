namespace CUBE_Dev_App_Server_API.Models;

public class SupplierCommand
{
    public int          PkSupplierCommand { get; set; }

    public DateTime     BuyingDate { get; set; }    = DateTime.Now;
    
    public float        TotalCost { get; set; }
    public float        TransportCost { get; set; }

    public Supplier     Supplier { get; set; }      = new();

    public Product[]    Products { get; set; }      = Array.Empty<Product>();
}
