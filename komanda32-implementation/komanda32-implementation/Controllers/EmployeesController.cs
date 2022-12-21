using komanda32_implementation.Database;
using komanda32_implementation.Models;
using komanda32_implementation.Models.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Controllers;

[ApiController]
public class EmployeesController : Controller
{
    private readonly DataContext _dbContext;

    public EmployeesController(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("employee/create")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateWorker createWorkerModel)
    {
        // add check if current user can create employee and is manager
        if (createWorkerModel == null)
            return new BadRequestResult();

        Worker worker = new Worker();
        worker.EmployeeCode = Guid.NewGuid().ToString();
        worker.Email = createWorkerModel.Email;
        worker.Name = createWorkerModel.Name;
        worker.Surname = createWorkerModel.Surname;
        worker.PhoneNumber = createWorkerModel.PhoneNumber;
        worker.Employment = createWorkerModel.Employment ?? 0;
        worker.BankAccount = createWorkerModel.BankAccount;
        worker.DateOfBirth = createWorkerModel.DateOfBirth ?? DateTime.MinValue;
        worker.Address = createWorkerModel.Address;
        worker.Type = createWorkerModel.Type ?? WorkerType.Cashier;

        await _dbContext.Workers.AddAsync(worker);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("employee/update")]
    public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] CreateWorker updateWorkerModel)
    {
        // add check if current user can update employee and is manager
        var employeeAccount = await _dbContext.Workers.SingleOrDefaultAsync(p => p.Id == employeeId);
        if (employeeAccount == null)
            return NotFound();

        employeeAccount.Email = updateWorkerModel.Email ?? employeeAccount.Email;
        employeeAccount.Name = updateWorkerModel.Name ?? employeeAccount.Name;
        employeeAccount.Surname = updateWorkerModel.Surname ?? employeeAccount.Surname;
        employeeAccount.Employment = updateWorkerModel.Employment ?? employeeAccount.Employment;
        employeeAccount.BankAccount = updateWorkerModel.BankAccount ?? employeeAccount.BankAccount;
        employeeAccount.Type = updateWorkerModel.Type ?? employeeAccount.Type;
        employeeAccount.Address = updateWorkerModel.Address ?? employeeAccount.Address;
        employeeAccount.DateOfBirth = updateWorkerModel.DateOfBirth ?? employeeAccount.DateOfBirth;
        employeeAccount.PhoneNumber = updateWorkerModel.PhoneNumber ?? employeeAccount.PhoneNumber;

        _dbContext.Workers.Update(employeeAccount);
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
            return NotFound();

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
            return NotFound();

        employee.Type = workerType;
        _dbContext.Workers.Update(employee);
        await _dbContext.SaveChangesAsync();
        return Ok();
    } 
}