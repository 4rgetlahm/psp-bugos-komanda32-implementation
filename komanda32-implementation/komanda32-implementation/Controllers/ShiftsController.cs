using komanda32_implementation.Database;
using komanda32_implementation.Models;
using komanda32_implementation.Models.Create;
using komanda32_implementation.Models.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Controllers;

public class ShiftsController : Controller
{
    private readonly DataContext _dbContext;

    public ShiftsController(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("employee/{id}/shift/create")]
    public async Task<IActionResult> CreateShift([FromBody] CreateShift createShiftModel)
    {
        // add check if current user can create shift and is manager
        Worker? employee = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == createShiftModel.WorkerId);
        if (employee == null)
        {
            return NotFound();
        }

        Shift shift = new Shift();
        shift.WorkerId = employee.Id;
        shift.StartTime = createShiftModel.StartTime;
        shift.EndTime = createShiftModel.EndTime;
        shift.GroupId = createShiftModel.GroupId;
        shift.LocationId = createShiftModel.LocationId;

        await _dbContext.Shifts.AddAsync(shift);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("employee/{id}/shift/update")]
    public async Task<IActionResult> UpdateShift([FromBody] UpdateShift updateShiftModel)
    {
        // add check if current user can update shift and is manager
        Worker? employee = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == updateShiftModel.WorkerId);
        if (employee == null)
            return NotFound();

        Shift shift = new Shift();
        shift.WorkerId = updateShiftModel.WorkerId ?? shift.WorkerId;
        shift.StartTime = updateShiftModel.StartTime ?? shift.StartTime;
        shift.EndTime = updateShiftModel.EndTime ?? shift.EndTime;
        shift.GroupId = updateShiftModel.GroupId ?? shift.GroupId;
        shift.LocationId = updateShiftModel.LocationId ?? shift.LocationId;

        _dbContext.Shifts.Update(shift);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("employee/{id}/shift/delete")]
    public async Task<IActionResult> DeleteShift(int shiftId)
    {
        // add check if current user can delete shift and is manager
        Shift? shift = await _dbContext.Shifts.SingleOrDefaultAsync(p => p.ShiftId == shiftId);
        if (shift == null)
            return NotFound();

        _dbContext.Shifts.Remove(shift);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
