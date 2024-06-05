using BibliotecaAuth.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;
using BibliotecaAuth.Utils;
using BibliotecaAuth.Classes;
using WebApi.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly AuthPassword _authPassword;

        public AuthController(
            IConfiguration configuration,
            IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
            _authPassword = new AuthPassword();
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResultApi> Registrarse(RegisterDTO register, CancellationToken cancellationToken)
        {
            if (register.Clave1 != register.Clave2)
                return new ActionResultApi(400, "Las Contraseñas no Coinciden");

            (string hash, string salt) = _authPassword.HashPassword(register.Clave1);

            var newRegister = new NewRegister
            {
                Id = Guid.NewGuid().ToString(),
                Email = register.Email.ToLower(),
                Usuario = register.Email.ToLower(),
                AuthHash = hash,
                AuthSalt = salt,
            };

            try
            {
                var respDB = await _authService.Register(newRegister, cancellationToken);

                if (respDB.StatusCode != 201)
                    return new ActionResultApi(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi(201, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResultApi<AuthToken>> IniciarSesion(LoginDTO login, CancellationToken cancellationToken)
        {
            var loginCredential = await _authService.Login(login.Email, cancellationToken);

            if (loginCredential == null)
                return new ActionResultApi<AuthToken>(401, "Usuario o Contraseña Incorrecta");

            if (!_authPassword.VerifyPassword(login.Clave, loginCredential.AuthHash, loginCredential.AuthSalt))
                return new ActionResultApi<AuthToken>(401, "Usuario o Contraseña Incorrecta");

            var authJwt = _configuration.GetSection("JWT").Get<AuthJwt>()!;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Sub, authJwt.Subject),
                new Claim(JwtRegisteredClaimNames.Name, login.Email),
                new Claim(JwtRegisteredClaimNames.Email, login.Email),
                new Claim(ClaimTypes.Role, loginCredential.Perfil)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authJwt.Key));
            var singIng = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: authJwt.Issuer,
                    audience: authJwt.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(authJwt.ExpireMin)),
                    signingCredentials: singIng
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var prueba = new AuthToken
            {
                ExpireMin = authJwt.ExpireMin,
                Token = tokenString
            };

            return new ActionResultApi<AuthToken>(200, "Usuario Ingresado Correctamente", prueba);
        }
    }
}
