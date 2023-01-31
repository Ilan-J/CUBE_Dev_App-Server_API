namespace CUBE_Dev_App_Server_API.Models;

public class SupplierCommand
{
    public int IDSupplierCommand { get; set; }
    public DateTime CommandDate { get; set; }
    public CommandType CommandType { get; set; } = new();
    public CommandStatus Status { get; set; } = new();
    public float TotalCost { get; set; }
    public float TransportCost { get; set; }
    public Supplier Supplier { get; set; } = new();
}
