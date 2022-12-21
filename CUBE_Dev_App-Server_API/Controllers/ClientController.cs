using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    public ClientController()
    {
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        if (!ClientService.GetAll(out List<Client> clients))
            return StatusCode(500);

        return Ok(clients);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if(!ClientService.Get(id, out Client? client))
            return StatusCode(500);

        if (client is null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public IActionResult Create(Client client)
    {
        if (!ClientService.Add(client))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkClient = client.PkClient }, client);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Client client)
    {
        if (id != client.PkClient)
            return BadRequest();

        if (!ClientService.Get(id, out Client? existingClient))
            return StatusCode(500);

        if (existingClient is null)
            return NotFound();

        if (!ClientService.Update(client))
            return StatusCode(500);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!ClientService.Get(id, out Client? client))
            return StatusCode(500);

        if (client is null)
            return NotFound();

        if (!ClientService.Delete(id))
            return StatusCode(500);

        return NoContent();
    }
}
