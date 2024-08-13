using BibliotecaPasswordManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Route("api/core")]
    [ApiController]
    public class PM_CoreController : ControllerBase
    {
        private readonly ICoreService _service;

        public PM_CoreController(ICoreService service)
        {
            _service = service;
        }
         
        [HttpGet("{deco},{id_usuario}")]
        public async Task<IActionResultApi<IEnumerable<CoreDTO_Get>>> GetAll(string deco, string id_usuario, CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetAll(deco, id_usuario, cancellationToken);

                return new ActionResultApi<IEnumerable<CoreDTO_Get>>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<IEnumerable<CoreDTO_Get>>(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("{deco}")]
        public async Task<IActionResultApi> Insert(string deco, CoreDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Insert(deco, dto, cancellationToken);

                if (respDB.StatusCode != 201)
                    return new ActionResultApi(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi(201, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{id},{deco}")]
        public async Task<IActionResultApi> Update(int id, string deco, CoreDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Update(id, deco, dto, cancellationToken);

                if (respDB.StatusCode != 202)
                    return new ActionResultApi(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi(202, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id},{id_usuario}")]
        public async Task<IActionResultApi> Delete(int id, string id_usuario, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Delete(id, id_usuario, cancellationToken);

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
