using ApplicationClassLibrary.DTOs;

namespace ApplicationClassLibrary.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApiDTO<object>> RegisterAsync(AuthRegisterDTO registerDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<AuthUserDTO>> LoginAsync(AuthLoginDTO loginDTO, CancellationToken cancellationToken);
    }
}
