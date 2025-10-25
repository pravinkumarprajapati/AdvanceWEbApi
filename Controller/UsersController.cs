using AdvanceWebApi.Model;
using AdvanceWEbApi.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdvanceWebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(
        ILogger<TokenController> logger,
        UserManager<IdentityUser> userManager) : ControllerBase
    {

        private readonly ILogger<TokenController> _logger = logger;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userExists = await _userManager.FindByNameAsync(register.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });
            }
            RegisterModel user = new()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName,

            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new { Status = "Success", Message = "User created successfully!" });
        }
    }
}
