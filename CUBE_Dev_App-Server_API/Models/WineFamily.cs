using System.Text.Json.Serialization;

namespace CUBE_Dev_App_Server_API.Models;

public class WineFamily
{
    public int idWineFamily { get; set; }
    public string Name { get; set; } = string.Empty;
}
