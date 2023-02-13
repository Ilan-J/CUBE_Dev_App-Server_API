namespace CUBE_Dev_App_Server_API.Models;

public class ClientCommand
{
    public int IDClientCommand { get; set; }
    public DateTime CommandDate { get; set; }
    public decimal TotalCostTTC { get; set; }
    public decimal TotalCostHT { get; set; }
    public int IDClient { get; set; }
    public int IDCommandStatus { get; set; }

    public Client_DTO Client { get; set; } = new();
    public List<ClientCommandList> ClientCommandList { get; set; } = new();
    public CommandStatus CommandStatus { get; set; } = new();
}