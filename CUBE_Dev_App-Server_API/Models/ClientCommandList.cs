namespace CUBE_Dev_App_Server_API.Models;

public class ClientCommandList
{
    public int Quantity { get; set; }
    public Article Article { get; set; } = new();
    public ClientCommand ClientCommand { get; set; } = new();
}
