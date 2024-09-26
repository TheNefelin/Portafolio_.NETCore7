using ClassLibraryApplication.DTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApiDTO<object>> RegisterAsync(RegisterDTO registerDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<UserDTO>> LoginAsync(LoginDTO loginDTO, CancellationToken cancellationToken);
    }
}
