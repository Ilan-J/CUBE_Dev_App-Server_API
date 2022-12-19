namespace CUBE_Dev_App_Server_API.Models;

public class Client
{
    public int      PkClient { get; set; }

    public string   Email { get; set; } =       string.Empty;
    public string   Password { get; set; } =    string.Empty;

    public string   Firstname { get; set; } =   string.Empty;
    public string   Lastname { get; set; } =    string.Empty;

    public string   Address { get; set; } =     string.Empty;
    public string   City { get; set; } =        string.Empty;
    public string   Region { get; set; } =      string.Empty;
    public string   PostalCode { get; set; } =  string.Empty;
    public string   Country { get; set; } =     string.Empty;
}
