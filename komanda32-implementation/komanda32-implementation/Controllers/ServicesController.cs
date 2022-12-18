using komanda32_implementation.Database;
using komanda32_implementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Controllers;

[ApiController]
public class ServicesController : Controller
{
    private readonly DataContext _dbContext;

    public ServicesController(DataContext dbContext )
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
    {
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
        // add check if current user can get product and is manager
        Service? product = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpGet]
    [Route("services")]
    public async Task<IActionResult> GetProducts(int top, string continuationToken)
    {
        // add check if current user can get products and is manager
        List<Service>? products = await _dbContext.Services.ToListAsync();
        return Ok(products);
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