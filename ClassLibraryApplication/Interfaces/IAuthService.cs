using ClassLibraryApplication.DTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApiDTO<object>> RegisterAsync(RegisterDTO registerDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<LoggedinDTO>> LoginAsync(LoginDTO loginDTO, JwtConfigDTO jwtConfigDTO, CancellationToken cancellationToken);
    }
}
