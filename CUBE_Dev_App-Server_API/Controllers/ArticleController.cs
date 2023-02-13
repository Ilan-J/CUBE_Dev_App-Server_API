using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to articles
/// </summary>
[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    /// <summary>
    /// Gets the list of every articles
    /// </summary>
    [HttpGet()]
    public IActionResult GetAll()
    {
        if (!ArticleService.GetAll(out List<Article> articles))
            return StatusCode(500);

        return Ok(articles);
    }

    /// <summary>
    /// Gets the data of a specified article
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!ArticleService.Get(id, out Article? article))
            return StatusCode(500);

        if (article == null)
            return NotFound();

        return Ok(article);
    }

    /// <summary>
    /// Gets the list of every articles from the specified wine family 
    /// </summary>
    [HttpGet("ByWineFamily")]
    public IActionResult GetWineFamilies(int id)
    {
        if (!WineFamilyService.Get(id, out WineFamily? wineFamily))
            return StatusCode(500);

        if (wineFamily == null)
            return NotFound();

        if (!ArticleService.GetAllSameFamily(id, out List <Article> articles))
            return StatusCode(500);

        return Ok(articles);
    }

    /// <summary>
    /// Gets the list of every articles from the specified supplier 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("BySupplier")]
    public IActionResult GetSuppliers(int id)
    {
        if (!SupplierService.Get(id,out Supplier? supplier))
            return StatusCode(500);

        if (supplier == null)
            return NotFound();

        if (!ArticleService.GetAllSameSupplier(id, out List<Article> articles))
            return StatusCode(500);

        return Ok(articles);
    }

    /// <summary>
    /// Creates a new article in the database
    /// </summary>
    /// <remarks>
    /// Sample request :
    /// 
    ///     POST
    ///     {
    ///           "reference": "string",
    ///           "name": "string",
    ///           "wineYear": 0,
    ///           "boxStockQuantity": 0,
    ///           "unitStockQuantity": 0,
    ///           "boxMinQuantity": 0,
    ///           "boxOptimalQuantity": 0,
    ///           "boxBuyingPrice": 0,
    ///           "unitPriceTTC": 0,
    ///           "boxPriceTTC": 0,
    ///           "tva": 0,
    ///           "bottleQuantityPerBox": 0,
    ///           "description": "string",
    ///           "imageLink": "string",
    ///           "idSupplier": 1,
    ///           "idWineFamily": 3
    ///     }
    /// </remarks>
    [HttpPost()]
    public IActionResult Create(Article article)
    {
        if (!ArticleService.Add(article))
            return StatusCode(500);

        return CreatedAtAction(nameof(Create), new { pkArticle = article.IDArticle }, article);
    }

    /// <summary>
    /// Updates a specified article
    /// </summary>
    /// <remarks>
    /// Sample request :
    /// 
    ///     PUT
    ///     int id,
    ///     {
    ///           "idArticle": 0,
    ///           "reference": "string",
    ///           "name": "string",
    ///           "wineYear": 0,
    ///           "boxStockQuantity": 0,
    ///           "unitStockQuantity": 0,
    ///           "boxVirtualQuantity": 0,
    ///           "unitVirtualQuantity": 0,
    ///           "boxMinQuantity": 0,
    ///           "boxOptimalQuantity": 0,
    ///           "boxBuyingPrice": 0,
    ///           "unitPriceTTC": 0,
    ///           "boxPriceTTC": 0,
    ///           "tva": 0,
    ///           "bottleQuantityPerBox": 0,
    ///           "description": "string",
    ///           "imageLink": "string",
    ///           "idSupplier": 1,
    ///           "idWineFamily": 3
    ///     }
    /// </remarks>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Article article)
    {
        if (id != article.IDArticle)
            return BadRequest();

        if (!ArticleService.Get(id, out Article? existingArticle))
            return StatusCode(500);

        if (existingArticle == null)
            return NotFound();

        if (!ArticleService.Update(article))
            return StatusCode(500);

        return NoContent();
    }
}
