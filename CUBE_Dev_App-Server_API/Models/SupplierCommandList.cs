namespace CUBE_Dev_App_Server_API.Models;

public class SupplierCommandList
{
    public int Quantity { get; set; }
    public Article Article { get; set; } = new();
    public SupplierCommand SupplierCommand { get; set; } = new();
}
