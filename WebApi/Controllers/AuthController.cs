using Microsoft.AspNetCore.Mvc;
using ClassLibraryApplication.Interfaces;
using ClassLibraryDTOs;

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
        public async Task<IActionResult> Register(RegisterDTO registerDTO, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(registerDTO, cancellationToken);

            if (response.StatusCode == 201)
                return CreatedAtAction(nameof(Register), response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ResponseApiDTO<LoggedinDTO>>> Login(LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            var jwtConfig = new JwtConfigDTO()
            {
                Key = _configuration["Jwt:Key"]!,
                Issuer = _configuration["Jwt:Issuer"]!,
                Audience = _configuration["Jwt:Audience"]!,
                Subject = _configuration["JWT:Subject"]!,
                ExpireMin = _configuration["JWT:ExpireMin"]!
            };

            var response = await _authService.LoginAsync(loginDTO, jwtConfig, cancellationToken);

            if (response.StatusCode == 201)
                return CreatedAtAction(nameof(Register), response);

            return StatusCode(response.StatusCode, response);
        }
    }
}
