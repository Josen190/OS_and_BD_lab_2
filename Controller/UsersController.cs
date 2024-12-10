using Microsoft.AspNetCore.Mvc;
using OS_and_BD_lab_2.Models;
using OS_and_BD_lab_2.Service;

namespace OS_and_BD_lab_2.Controller
{
    public class Token
    {
        public string token { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get/{login}")]
        public async Task<IActionResult> GetUser(string login)
        {
            var user = await _userService.GetUserByLoginAsync(login);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User registerUser)
        {
            if (string.IsNullOrEmpty(registerUser.Login) || string.IsNullOrEmpty(registerUser.Password))
            {
                return BadRequest(new { Message = "Login and password are required." });
            }

            var result = await _userService.RegisterUserAsync(registerUser);

            if (!result)
            {
                return Conflict(new { Message = "Login is already taken." });
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto, [FromServices] TokenService tokenService)
        {
            var user = await _userService.AuthenticateUserAsync(loginUserDto.Login, loginUserDto.Password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            var token = tokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] Token token, [FromServices] TokenRevocationService revocationService)
        {
            revocationService.RevokeToken(token.token);
            return Ok();
        }


    }

}
