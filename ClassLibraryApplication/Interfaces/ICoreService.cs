using ClassLibraryDTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface ICoreService
    {
        Task<ResponseApiDTO<IEnumerable<CoreDTO>>> GetAllAsync(string sqlToken, string idUsuario, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> GetByIdAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> InsertAsync(string sqlToken, CoreDTO coreDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> UpdateAsync(string sqlToken, CoreDTO coreDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> DeleteAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken);
    }
}
