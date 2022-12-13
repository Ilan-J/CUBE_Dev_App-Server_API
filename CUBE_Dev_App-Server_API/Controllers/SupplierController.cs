using CUBE_Dev_App_Server_API.DBQueries;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(ILogger<SupplierController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetSupplier")]
    public IEnumerable<Supplier> Get()
    {
        Supplier[]? suppliers = QSuppliers.SelectAll();
        if (suppliers is null)
        {
            return Enumerable.Empty<Supplier>();
        }
        return suppliers;
    }
    [HttpPut(Name = "PutSupplier")]
    public void Put(Supplier supplier)
    {

    }
}
