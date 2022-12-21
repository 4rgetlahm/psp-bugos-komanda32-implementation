using komanda32_implementation.Database;
using komanda32_implementation.Models;
using komanda32_implementation.Models.Create;
using komanda32_implementation.Models.Update;
using komanda32_implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace komanda32_implementation.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly DataContext _dataContext;
        public CustomerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomer createCustomerModel, CancellationToken cancellationToken)
        {
            if(createCustomerModel == null)
            {
                throw new ArgumentNullException(nameof(createCustomerModel));
            }
            Franchise franchise = await _dataContext.Franchises.FindAsync(createCustomerModel.FranchiseId); 
            if(franchise == null)
            {
                return BadRequest(new ErrorResponse("Franchise not found"));
            }
            Customer customer = await _dataContext.Customers
                .Where(customer => customer.Username == createCustomerModel.Username || customer.Email == createCustomerModel.Email)
                .FirstOrDefaultAsync(cancellationToken);
            
            if(customer != null)
            {
                return BadRequest(new ErrorResponse("Customer already exists"));
            }

            customer = new Customer(createCustomerModel.Username, Authentication.Instance.GetHash(createCustomerModel.Password), createCustomerModel.Email);
            customer.Franchise = franchise;

            _dataContext.Customers.Add(customer);
            _dataContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
        {
            Customer customer = await _dataContext.Customers
                .Include(customer => customer.Franchise)
                .Where(customer => customer.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if (customer == null)
            {
                return NotFound(new ErrorResponse("Customer not found"));
            }
            return Ok(customer);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomer updateCustomerModel, CancellationToken cancellationToken)
        {
            if(updateCustomerModel == null)
            {
                return BadRequest(new ErrorResponse("Invalid request"));
            }
            Customer customer = await _dataContext.Customers
                .Include(customer => customer.Franchise)
                .Where(customer => customer.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if (customer == null)
            {
                return NotFound(new ErrorResponse("Customer not found"));
            }

            if (updateCustomerModel.FranchiseId != null)
            {
                Franchise franchise = await _dataContext.Franchises.FindAsync(updateCustomerModel.FranchiseId);
                if(franchise == null)
                {
                    return BadRequest(new ErrorResponse("Franchise doesn't exist"));
                }
                customer.Franchise = franchise;
            }

            customer.Username = updateCustomerModel.Username ?? customer.Username;
            customer.Email = updateCustomerModel.Email ?? customer.Email;
            customer.Password = updateCustomerModel.Password != null ? Authentication.Instance.GetHash(updateCustomerModel.Password) : customer.Password;
            customer.Name = updateCustomerModel.Name ?? customer.Name;
            customer.Surname = updateCustomerModel.Surname ?? customer.Surname;
            customer.DeliveryAddress = updateCustomerModel.DeliveryAddress ?? customer.DeliveryAddress;
            customer.Blocked = updateCustomerModel.Blocked ?? customer.Blocked;

            _dataContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
        {
            Customer customer = await _dataContext.Customers
                .Include(customer => customer.Franchise)
                .Where(customer => customer.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if(customer == null)
            {
                return NotFound(new ErrorResponse("Customer not found"));
            }

            _dataContext.Customers.Remove(customer);
            _dataContext.SaveChanges();

            return Ok();
        }
    }
}
