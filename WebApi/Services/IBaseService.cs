using WebApi.Utils;

namespace WebApi.Services
{
    public interface IBaseService<DtoGet, DtoPostPut>
    {
        public Task<IEnumerable<DtoGet>> GetAll(CancellationToken cancellationToken);
        public Task<DtoGet?> GetById(int Id, CancellationToken cancellationToken);
        public Task<ResponseDB> Insert(DtoPostPut dto, CancellationToken cancellationToken);
        public Task<ResponseDB> Update(int Id, DtoPostPut dto, CancellationToken cancellationToken);
        public Task<ResponseDB> Delete(int Id, CancellationToken cancellationToken);
    }
}
