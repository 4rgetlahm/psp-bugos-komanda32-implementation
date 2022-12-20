using komanda32_implementation.Database;
using komanda32_implementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Controllers;

[ApiController]
public class LocationsController : Controller
{
    private readonly DataContext _dbContext;

    public LocationsController(DataContext dbContext )
    {
        _dbContext = dbContext;
    }

    [HttpPatch]
    [Route("location/{id}")]
    public async Task<IActionResult> UpdateLocationDescription (int id, string description) // as per now, havent found direct connection of user to the location
    {
        // add check if current user can update location description and is manager
        Group? location = await _dbContext.Groups.SingleOrDefaultAsync(p => p.Id == id);
        if (location == null)
        {
            return NotFound();
        }   

        location.Description = description;
        _dbContext.Groups.Update(location);
        await _dbContext.SaveChangesAsync();
        return Ok();
    } 
}