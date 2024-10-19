using ClassLibraryDTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface ICoreService
    {
        Task<ResponseApiDTO<CoreIVDTO>> RegisterAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreIVDTO>> LoginAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken);
        Task<ResponseApiDTO<IEnumerable<CoreDTO>>> GetAllAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> GetByIdAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> InsertAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken);
        Task<ResponseApiDTO<CoreDTO>> UpdateAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken);
        Task<ResponseApiDTO<object>> DeleteAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken);
    }
}
