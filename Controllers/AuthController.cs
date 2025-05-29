using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace my_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok(new { message = "Giriş başarılı", user = User.Identity.Name });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Burada örnek olarak sabit kullanıcı adı/şifre kullanıyoruz
            if (request.Username == "admin" && request.Password == "1234")
            {
                var jwtConfig = _configuration.GetSection("Jwt");
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var token = new JwtSecurityToken(
                    issuer: jwtConfig["Issuer"],
                    audience: jwtConfig["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(double.Parse(jwtConfig["ExpireMinutes"])),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        [JsonPropertyName("Username")]
        public string Username { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}

