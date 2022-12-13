namespace CUBE_Dev_App_Server_API;

public class Supplier
{
    public Supplier(int pk_upplier, string name, string email, string address, string city, string postalCode)
    {
        Pk_upplier = pk_upplier;
        Name = name;
        Email = email;
        Address = address;
        City = city;
        PostalCode = postalCode;
    }

    public int Pk_upplier { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }

    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
}
