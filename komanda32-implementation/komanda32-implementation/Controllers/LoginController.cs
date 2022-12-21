using komanda32_implementation.Database;
using komanda32_implementation.Models;
using komanda32_implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace komanda32_implementation.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly DataContext _dataContext;
        public LoginController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLoginModel)
        {
            if (userLoginModel == null)
            {
                return BadRequest();
            }
            IAuthenticatable authenticatable = _dataContext.Customers
                .Where(customer => customer.Username == userLoginModel.Username && customer.Password == Authentication.Instance.GetHash(userLoginModel.Password))
                .FirstOrDefault();
            if(authenticatable == null)
            {
                return Unauthorized();
            }
            return Ok(new {token = Authentication.Instance.GenerateJSONWebToken(authenticatable)});
        }
    }
}
