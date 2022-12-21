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
    public async Task<IActionResult> CreateService([FromBody] Service product)
    {
        // add check if current user can create product and is manager
        await _dbContext.Services.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("services/{id}/update")]
    public async Task<IActionResult> UpdateService(int id, [FromBody] Service product)
    {//?? does not work as expected
        // add check if current user can update product and is manager
        Service? serviceToUpdate = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id);
        if (serviceToUpdate == null)
            return NotFound();

        _dbContext.Services.Update(product);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    [Route("services/{id}")]
    public async Task<IActionResult> GetService(int id)
    {
        Service? service = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id);
        if (service == null)
            return NotFound();

        return Ok(service);
    }

    [HttpGet]
    [Route("services")]
    public async Task<ActionResult<ContinuationTokenResult<IEnumerable<Service>>>> GetServices(int top, string? continuationToken = null)
    {
        if (top > 1000)
            return new BadRequestObjectResult("Top value cannot be greater than 1000");
        if (top < 0)
            return new BadRequestObjectResult("Top value cannot be negative");

        List<Service> services;
        int updatedContinuationTokenValue = top;
        var valueBytes = Convert.FromBase64String(continuationToken ?? "");

        if (int.TryParse(System.Text.Encoding.UTF8.GetString(valueBytes), out var decodedSkipValue))
        {
            services = await _dbContext.Services.Skip(decodedSkipValue).Take(top).ToListAsync();
            updatedContinuationTokenValue += decodedSkipValue;
        }
        else
        {
            services = await _dbContext.Services.Take(top).ToListAsync();
        }

        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(updatedContinuationTokenValue.ToString());
        return new ContinuationTokenResult<IEnumerable<Service>>
        {
            Response = services,
            ContinuationToken = services.Count == top ? Convert.ToBase64String(plainTextBytes) : ""
        };
    }

    [HttpDelete]
    [Route("services/{id}/delete")]
    public async Task<IActionResult> DeleteService(int id)
    {
        // add check if current user can delete product and is manager
        Service? service = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id);
        if (service == null)
            return NotFound();

        _dbContext.Services.Remove(service);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}