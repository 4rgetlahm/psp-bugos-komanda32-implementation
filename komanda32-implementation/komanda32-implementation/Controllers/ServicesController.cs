using komanda32_implementation.Database;
using komanda32_implementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Controllers;

[ApiController]
public class ServicesController : Controller
{
    private readonly DataContext _dbContext;

    public ServicesController(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("services/create")]
    public async Task<IActionResult> CreateProduct([FromBody] Service product)
    {
        // add check if current user can create product and is manager
        await _dbContext.Services.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("services/{id}/update")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Service product)
    {//?? does not work as expected
        // add check if current user can update product and is manager
        Service? productToUpdate = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id);
        if (productToUpdate == null)
        {
            return NotFound();
        }

        _dbContext.Services.Update(product);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    [Route("services/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        Service? product = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet]
    [Route("services")]
    public async Task<ActionResult<ContinuationTokenResult<IEnumerable<Service>>>> GetProducts(int top, string? continuationToken = null)
    {
        if (top > 1000)
            return new BadRequestObjectResult("Top value cannot be greater than 1000");
        if (top < 0)
            return new BadRequestObjectResult("Top value cannot be negative");

        List<Service> products;
        int updatedContinuationTokenValue = top;
        var valueBytes = Convert.FromBase64String(continuationToken ?? "");

        if (int.TryParse(System.Text.Encoding.UTF8.GetString(valueBytes), out var decodedSkipValue))
        {
            products = await _dbContext.Services.Skip(decodedSkipValue).Take(top).ToListAsync();
            updatedContinuationTokenValue += decodedSkipValue;
        }
        else
        {
            products = await _dbContext.Services.Take(top).ToListAsync();
        }

        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(updatedContinuationTokenValue.ToString());
        return new ContinuationTokenResult<IEnumerable<Service>>
        {
            Response = products,
            ContinuationToken = products.Count == top ? Convert.ToBase64String(plainTextBytes) : ""
        };
    }

    [HttpDelete]
    [Route("services/{id}/delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        // add check if current user can delete product and is manager
        Service? product = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        _dbContext.Services.Remove(product);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}