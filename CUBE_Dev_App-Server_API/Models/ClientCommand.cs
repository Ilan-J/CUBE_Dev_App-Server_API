﻿namespace CUBE_Dev_App_Server_API.Models;

public class ClientCommand
{
    public int              PkClientCommand { get; set; }

    public DateTime         CommandDate     { get; set; }
    public CommandStatus    CommandStatus   { get; set; }

    public string           Address         { get; set; } = string.Empty;
    public string           City            { get; set; } = string.Empty;
    public string           Region          { get; set; } = string.Empty;
    public string           PostalCode      { get; set; } = string.Empty;
    public string           Country         { get; set; } = string.Empty;

    public float            TotalCost       { get; set; }
    public float            TransportCost   { get; set; }

    public Client           Client          { get; set; } = new();
}
