using ClassLibraryDTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface IBaseCRUDService<T>
    {
        Task<ResponseApiDTO<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ResponseApiDTO<T>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ResponseApiDTO<T>> InsertAsync(T dto, CancellationToken cancellationToken);
        Task<ResponseApiDTO<T>> UpdateAsync(T dto, CancellationToken cancellationToken);
        Task<ResponseApiDTO<T>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
