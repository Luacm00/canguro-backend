using Canguro.API.Models;
using Canguro.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Canguro.API.Data;
using System.Threading.Tasks;

namespace Canguro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserRespository _userRepo;

        public AuthController(IOptions<JwtSettings> jwtSettings, UserRespository userRepo)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password))
                return BadRequest("Usuario y contraseña son requeridos");

            var user = await _userRepo.ValidateUserAsync(loginUser.Username, loginUser.Password);
            if (user == null || !user.Activo)
                return Unauthorized("Credenciales inválidas");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}
