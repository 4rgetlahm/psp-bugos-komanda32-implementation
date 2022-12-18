using komanda32_implementation.Database;
using komanda32_implementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Controllers;

[ApiController]
public class EmployeesController : Controller
{
    private readonly DataContext _dbContext;

    public EmployeesController(DataContext dbContext )
    {
        _dbContext = dbContext;
    }
    
    [HttpPost]
    [Route("employee/create")]
    public async Task<IActionResult> CreateEmployee([FromBody] Worker employee)
    {
        // add check if current user can create employee and is manager
        
        employee.EmployeeCode = Guid.NewGuid().ToString();
        await _dbContext.Workers.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPatch]
    [Route("employee/update")]
    public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] Worker employee)
    {
        // add check if current user can update employee and is manager
        Worker? employeeAccount = await _dbContext.Workers.SingleOrDefaultAsync(p=> p.Id == employeeId);
        _dbContext.Workers.Update(employee);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete]
    [Route("employee/delete")]
    public async Task<IActionResult> DeleteEmployee(int employeeId)
    {
        // add check if current user can delete employee and is manager
        Worker? employee = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == employeeId);
        if (employee == null)
        {
            return NotFound();
        }

        _dbContext.Workers.Remove(employee);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPatch]
    [Route("employee/type/assign/{id}")]
    public async Task<IActionResult> AssignEmployeeType(int id, WorkerType workerType)
    {
        // add check if current user can assign employee type and is manager
        Worker? employee = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        employee.Type = workerType;
        _dbContext.Workers.Update(employee);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
  
    
    [HttpPost]
    [Route("employee/{id}/shift/create")]
    public async Task<IActionResult> CreateShift(int id, [FromBody] Shift shift)
    {
        // add check if current user can create shift and is manager
        Worker? employee = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        shift.WorkerId = employee.Id;
        await _dbContext.Shifts.AddAsync(shift);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPatch]
    [Route("employee/{id}/shift/update")]
    public async Task<IActionResult> UpdateShift(int id, [FromBody] Shift shift)
    {
        // add check if current user can update shift and is manager
        Worker? employee = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        _dbContext.Shifts.Update(shift);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete]
    [Route("employee/{id}/shift/delete")]
    public async Task<IActionResult> DeleteShift(int id, int shiftId)
    {
        // add check if current user can delete shift and is manager
        Worker? employee = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        Shift? shift = await _dbContext.Shifts.SingleOrDefaultAsync(p => p.ShiftId == shiftId);
        if (shift == null)
        {
            return NotFound();
        }

        _dbContext.Shifts.Remove(shift);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}