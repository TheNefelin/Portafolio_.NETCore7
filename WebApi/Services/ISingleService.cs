using WebApi.Utils;

namespace WebApi.Services
{
    public interface ISingleService<DTO>
    {
        public Task<IEnumerable<DTO>> GetAll(CancellationToken cancellationToken);
        public Task<ResponseDB> Insert(DTO dto, CancellationToken cancellationToken);
        public Task<ResponseDB> Delete(DTO dto, CancellationToken cancellationToken);
    }
}
