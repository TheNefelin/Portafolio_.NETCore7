using BibliotecaAuth.Classes;
using BibliotecaPortafolio.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Utils;

namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Authorize(Roles = UserRole.Admin)]
    [Route("api/Enlace")]
    [ApiController]
    public class PFEnlaceController : ControllerBase
    {
        private readonly IBaseService<EnlaceDTO_Get, EnlaceDTO_PostPut> _service;

        public PFEnlaceController(IBaseService<EnlaceDTO_Get, EnlaceDTO_PostPut> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResultApi<IEnumerable<EnlaceDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetAll(cancellationToken);

                return new ActionResultApi<IEnumerable<EnlaceDTO_Get>>(200, "", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<IEnumerable<EnlaceDTO_Get>>(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResultApi<EnlaceDTO_Get>> GetById(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetById(Id, cancellationToken);

                if (respData == null)
                    return new ActionResultApi<EnlaceDTO_Get>(400, "El Id No Existe");

                return new ActionResultApi<EnlaceDTO_Get>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<EnlaceDTO_Get>(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResultApi<EnlaceDTO_Get>> Insert(EnlaceDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Insert(dto, cancellationToken);

                if (respDB.StatusCode != 201)
                    return new ActionResultApi<EnlaceDTO_Get>(respDB.StatusCode, respDB.Msge);

                var respData = await _service.GetById(respDB.Id, cancellationToken);
                return new ActionResultApi<EnlaceDTO_Get>(201, respDB.Msge, respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<EnlaceDTO_Get>(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResultApi<EnlaceDTO_Get>> Update(int Id, EnlaceDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Update(Id, dto, cancellationToken);

                if (respDB.StatusCode != 202)
                    return new ActionResultApi<EnlaceDTO_Get>(respDB.StatusCode, respDB.Msge);

                var respData = await _service.GetById(respDB.Id, cancellationToken);
                return new ActionResultApi<EnlaceDTO_Get>(202, respDB.Msge, respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<EnlaceDTO_Get>(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResultApi> Delete(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Delete(Id, cancellationToken);

                if (respDB.StatusCode != 202)
                    return new ActionResultApi(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi(202, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi(500, $"Error: {ex.Message}");
            }
        }
    }
}
