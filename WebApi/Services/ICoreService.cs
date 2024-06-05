using BibliotecaPasswordManager.DTOs;
using WebApi.Utils;

namespace WebApi.Services
{
    public interface ICoreService
    {
        Task<IEnumerable<CoreDTO_Get>> GetAll(string Id_Usuario, string Deco, CancellationToken cancellationToken);
        Task<ResponseDB> Insert(string Deco, CoreDTO_PostPut dto, CancellationToken cancellationToken);
        Task<ResponseDB> Update(int Id, string Deco, CoreDTO_PostPut dto, CancellationToken cancellationToken);
        Task<ResponseDB> Delete(int Id, string Id_Usuario, CancellationToken cancellationToken);
    }
}
