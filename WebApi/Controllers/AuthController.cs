using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClassLibraryApplication.Interfaces;
using ClassLibraryApplication.DTOs;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(
            IConfiguration configuration,
            IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(AuthRegisterDTO registerDTO, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(registerDTO, cancellationToken);

            if (response.StatusCode == 201)
                return CreatedAtAction(nameof(Register), response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(AuthLoginDTO loginDTO, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginAsync(loginDTO, cancellationToken);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response);

            if (response.Data is null)
                return StatusCode(500, "Error al procesar los datos del usuario.");

            var token = GenerateJwtToken(response.Data);

            return Ok(new
            {
                response,
                ExpireMin = _configuration["JWT:ExpireMin"],
                Token = token
            });
        }

        private string GenerateJwtToken(AuthUserDTO user)
        {
            // Define los claims (información contenida en el token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Genera una clave simétrica a partir del secret en appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Configuración del token: audiencia, emisor, expiración y firma
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:ExpireMin"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
