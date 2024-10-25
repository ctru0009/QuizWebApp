using DotNetEnv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Data;
using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QuizWebAppContext _context;

        public UsersController(QuizWebAppContext context)
        {
            _context = context;
        }
        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerUserDto.Username))
            {
                return BadRequest(new { message = "Username already exists" });
            }

            var user = new User
            {
                Username = registerUserDto.Username,
                Email = registerUserDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurrentUser), new { userId = user.UserId }, new UserDto
            {
                Username = user.Username,
                Email = user.Email
            });
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginUserDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // GET: api/users/me
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            // Get the user ID from the JWT token claims
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            // Fetch user from the database
            var user = await _context.Users
                .Where(u => u.UserId.ToString() == userId)
                .Select(u => new UserDto
                {
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        private string GenerateJwtToken(User user)
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Generate secret key 
            var secretKey = "This is secret key ahsdlkjashdljkashdklajshdajklhd"; // This should be add to the environment variable for security purpose.
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer: "quizapp",
                audience: "quizapp",
                claims: new[]
                {
                    new Claim("userId", user.UserId.ToString()),
                    new Claim("username", user.Username)
                },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
