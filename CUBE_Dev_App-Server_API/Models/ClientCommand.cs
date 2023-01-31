namespace CUBE_Dev_App_Server_API.Models;

public class ClientCommand
{
    public int IDClientCommand { get; set; }
    public DateTime CommandDate { get; set; }
    public CommandStatus Status { get; set; } = new();
    public Client Client { get; set; } = new();
}
