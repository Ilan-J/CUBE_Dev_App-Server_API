namespace CUBE_Dev_App_Server_API.Models;

public class SupplyList
{
    public SupplierCommand  SupplierCommand { get; set; }  = new SupplierCommand();
    public Product          Product { get; set; }   = new Product();

    public int              Quantity { get; set; }
}
