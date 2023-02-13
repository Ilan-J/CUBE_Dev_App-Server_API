using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to unique data ot the database
/// </summary>
[ApiController]
[Route("[controller]")]
public class UtilsController : ControllerBase
{
    /// <summary>
    /// Gets the default margin
    /// </summary>
    [HttpGet("DefaultMargin")]
    public IActionResult TryLogin()
    {
        if (!UtilsService.GetDefaultMargin(out string? defaultMargin))
            return StatusCode(500);

        return Ok(defaultMargin);
    }

    /// <summary>
    /// Gets the default TVA
    /// </summary>
    [HttpGet("DefaultTVA")]
    public IActionResult Change()
    {
        if (!UtilsService.GetDefaultTVA(out string? defaultTVA))
            return StatusCode(500);

        return Ok(defaultTVA);
    }

    /// <summary>
    /// Modify the value of the default margin
    /// </summary>
    [HttpPut("DefaultMargin")]
    public IActionResult ChangeMargin(decimal value)
    {
        if (!UtilsService.UpdateDefaultMargin(value))
            return StatusCode(500);

        return Ok();
    }

    /// <summary>
    /// Modify the value of the default TVA
    /// </summary>
    [HttpPut("DefaultTVA")]
    public IActionResult ChangeTVA(decimal value)
    {
        if (!UtilsService.UpdateDefaultTVA(value))
            return StatusCode(500);

        return Ok();
    }
}
