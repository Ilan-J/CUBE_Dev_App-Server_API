namespace CUBE_Dev_App_Server_API.Models;

public class SupplierCommand
{
    public int IDSupplierCommand { get; set; }
    public decimal TransportCost { get; set; }
    public DateTime CommandDate { get; set; }
    public decimal TotalCost { get; set; }
    public int IDSupplier { get; set; }
    public int IDCommandStatus { get; set; }
    public int IDCommandType { get; set; }

    public Supplier Supplier { get; set; } = new();
    public CommandStatus CommandStatus { get; set; } = new();
    public List<SupplierCommandList> SupplierCommandList { get; set; } = new();
    public CommandType CommandType { get; set; } = new();
}