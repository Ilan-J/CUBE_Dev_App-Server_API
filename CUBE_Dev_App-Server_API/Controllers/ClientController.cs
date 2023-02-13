using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to clients
/// </summary>
[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    /// <summary>
    /// Gets the list of every clients
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!ClientService.GetAll(out List<Client_DTO>? clients))
            return StatusCode(500);

        return Ok(clients);
    }

    /// <summary>
    /// Gets the data of a specified client
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!ClientService.Get(id, out Client_DTO? client))
            return StatusCode(500);

        if (client == null)
            return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// Creates a new client in the database
    /// </summary>
    /// <remarks>
    /// Sample request :
    ///
    ///     POST
    ///     {
    ///         "firstname": "string",
    ///         "lastname": "string",
    ///         "email": "string",
    ///         "address": "string",
    ///         "town": "string",
    ///         "postalCode": "string",
    ///         "country": "string",
    ///         "password": "string"
    ///     }
    /// </remarks>
    [HttpPost]
    public IActionResult Create(Client client)
    {
        if (string.IsNullOrEmpty(client.Password))
            return BadRequest("A password is required.");

        if (!ClientService.Add(client))
            return StatusCode(500);

        int idNewClient = DBConnection.GetHighestPrimaryKey("Client");

        client.IDClient = idNewClient;

        return CreatedAtAction(nameof(Create), new { client.IDClient }, client);
    }

    /// <summary>
    /// Updates a specified client
    /// </summary>
    /// <remarks>
    ///
    ///     PUT
    ///     int id,
    ///     string password,
    ///     {
    ///         "idClient": 0,
    ///         "firstname": "string",
    ///         "lastname": "string",
    ///         "email": "string",
    ///         "address": "string",
    ///         "town": "string",
    ///         "postalCode": "string",
    ///         "country": "string",
    ///         "password": "string"
    ///     }
    /// </remarks>
    [HttpPut]
    public IActionResult Update(int id, string password, Client client)
    {
        if (id != client.IDClient)
            return BadRequest("The IDClient field in the Client object must be the same as the ID put as a parameter.");

        if (string.IsNullOrEmpty(client.Password))
            return BadRequest("A password is required.");

        if (!ClientService.Get(id, out Client_DTO? existingClient))
            return StatusCode(500);

        if (existingClient == null)
            return NotFound();

        if (!LoginService.WebsiteLogin(id, password))
            return StatusCode(400, "Incorrect password.");

        if (!ClientService.Update(client))
            return StatusCode(500);

        return NoContent();
    }
}
