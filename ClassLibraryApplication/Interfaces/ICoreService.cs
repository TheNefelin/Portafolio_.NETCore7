using ClassLibraryApplication.DTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface ICoreService
    {
        Task<ResponseApiDTO<IEnumerable<CoreDTO>>> GetAllAsync(string sqlToken, string idUsuario, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> GetByIdAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> InsertAsync(string sqlToken, CoreDTO secretDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> UpdateAsync(string sqlToken, CoreDTO secretDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> DeleteAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken);
    }
}
