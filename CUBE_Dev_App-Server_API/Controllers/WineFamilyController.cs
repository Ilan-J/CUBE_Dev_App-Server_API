using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to wine families
/// </summary>
[ApiController]
[Route("[controller]")]
public class WineFamilyController : ControllerBase
{
    /// <summary>
    /// Gets the list of every wine families
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!WineFamilyService.GetAll(out List<WineFamily>? wineFamilies))
            return StatusCode(500);

        return Ok(wineFamilies);
    }

    /// <summary>
    /// Gets the data of a specified wine family
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!WineFamilyService.Get(id, out WineFamily? wineFamily))
            return StatusCode(500);

        if (wineFamily == null)
            return NotFound();

        return Ok(wineFamily);
    }

    /// <summary>
    /// Creates a new wine family in the database
    /// </summary>
    /// <remarks>
    /// Sample request :
    ///
    ///     POST
    ///     {
    ///         "name": "string"
    ///     }
    /// </remarks>
    [HttpPost]
    public IActionResult Create(WineFamily wineFamily)
    {
        if (!WineFamilyService.Add(wineFamily))
            return StatusCode(500);

        int id = DBConnection.GetHighestPrimaryKey("WineFamily");

        if (!WineFamilyService.Get(id, out WineFamily? wineFamilyCreated))
            return StatusCode(500);

        if (wineFamilyCreated == null)
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { wineFamilyCreated.idWineFamily }, wineFamilyCreated);
    }

    /// <summary>
    /// Updates a specified wine family
    /// </summary>
    /// <remarks>
    /// Sample request :
    ///
    ///     PUT
    ///     int id,
    ///     {
    ///         "idWineFamily": 0,
    ///         "name": "string"
    ///     }
    /// </remarks>
    [HttpPut("{id}")]
    public IActionResult Update(int id, WineFamily wineFamily)
    {
        if (id != wineFamily.idWineFamily)
            return BadRequest();

        if (!WineFamilyService.Get(id, out WineFamily? existingWineFamily))
            return StatusCode(500);

        if (existingWineFamily == null)
            return NotFound();

        if (!WineFamilyService.Update(wineFamily))
            return StatusCode(500);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specified wine family from the database
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!WineFamilyService.Get(id, out WineFamily? wineFamily))
            return StatusCode(500);

        if (wineFamily == null)
            return NotFound();

        if (!WineFamilyService.Delete(id))
            return StatusCode(500);

        return NoContent();
    }
}
