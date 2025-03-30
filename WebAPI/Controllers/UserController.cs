using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<AuthController> _logger;

        public UserController(IUserService userService, ILogger<AuthController> logger)
        {
            this.userService = userService;
            _logger = logger;
        }

        [HttpGet("GetUserById")]
        //[Authorize]
        public async Task<IActionResult> GetUserInformationById([FromQuery] int userId)
        {
            _logger.LogInformation("Get user information for user with id: {0}", userId);
            var user = await userService.GetUserByIdAsync(userId);
            if (user != null)
            {
                _logger.LogInformation("User with id {0} found", userId);
                return Ok(user);
            }
            _logger.LogInformation("User with id {0} not found", userId);
            return NotFound();
        }
    }
}
