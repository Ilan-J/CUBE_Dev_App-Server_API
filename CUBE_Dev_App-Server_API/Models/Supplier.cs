﻿namespace CUBE_Dev_App_Server_API.Models;

public class Supplier
{
    public int      PkSupplier  { get; set; }

    public string   Name        { get; set; } = string.Empty;
    public string   Email       { get; set; } = string.Empty;

    public string   Address     { get; set; } = string.Empty;
    public string   City        { get; set; } = string.Empty;
    public string   PostalCode  { get; set; } = string.Empty;
}
