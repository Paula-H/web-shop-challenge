using Microsoft.AspNetCore.Mvc;
using Domain.Dto;
using Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Domain.Dto.Create;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            this.authService = authService;
            _logger = logger;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            _logger.LogInformation("Login attempt for user: {0}", user.Email);
            var token = await authService.LoginAsync(user);
            if (token != null)
            {
                _logger.LogInformation("User {0} logged in successfully", user.Email);
                return Ok(new {token});
            }
            _logger.LogInformation("User {0} failed to log in", user.Email);
            return Unauthorized();
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserDto user)
        {
            try
            {
                _logger.LogInformation("Register attempt for user: {0}", user.Email);
                await authService.RegisterAsync(user);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to register user: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("ChangePassword")]
        // [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                _logger.LogInformation("Change password attempt for user: {0}", changePasswordDto.Id);
                await authService.ChangePassword(changePasswordDto);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to change password for user: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
