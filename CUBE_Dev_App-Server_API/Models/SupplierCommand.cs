namespace CUBE_Dev_App_Server_API.Models;

public class SupplierCommand
{
    public int                  PkSupplierCommand   { get; set; }

    public DateTime             CommandDate         { get; set; } = DateTime.Now;
    public CommandType          CommandType         { get; set; }
    public CommandStatus        CommandStatus       { get; set; }
    
    public float                TotalCost           { get; set; }
    public float                TransportCost       { get; set; }

    public Supplier             Supplier            { get; set; } = new();

    public List<ProductBought>  Products            { get; set; } = new();
}
