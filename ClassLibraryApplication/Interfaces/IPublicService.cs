using ClassLibraryApplication.DTOs;

namespace ClassLibraryApplication.Interfaces
{
    public interface IPublicService
    {
        Task<ResponseApiDTO<IEnumerable<UrlsGrpsDTO>>> GetAllUrlsGrpsAsync(CancellationToken cancellationToken);
        Task<ResponseApiDTO<IEnumerable<ProjectsDTO>>> GetAllProjectsAsync(CancellationToken cancellationToken);
    }
}
