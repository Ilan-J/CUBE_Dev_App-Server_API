using CUBE_Dev_App_Server_API.Models.Enums;

namespace CUBE_Dev_App_Server_API.Models;

public class ClientCommand
{
    public int IDClientCommand { get; set; }
    public DateTime CommandDate { get; set; }
    public CommandStatus Status { get; set; }
    public Client Client { get; set; } = new();
}
