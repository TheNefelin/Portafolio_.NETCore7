using ClassLibraryApplication.DTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface IUrlGrpService
    {
        Task<ResponseApiDTO<IEnumerable<SecretDTO>>> GetAllAsync(string sqlToken, string idUsuario, CancellationToken cancellationToken);
        Task<ResponseApiDTO<SecretDTO>> GetByIdAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken);
        Task<ResponseApiDTO<SecretDTO>> InsertAsync(string sqlToken, SecretDTO secretDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<SecretDTO>> UpdateAsync(string sqlToken, SecretDTO secretDTO, CancellationToken cancellationToken);
        Task<ResponseApiDTO<SecretDTO>> DeleteAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken);
    }
}
