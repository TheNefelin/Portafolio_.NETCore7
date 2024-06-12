using BibliotecaPasswordManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PM_CoreController : ControllerBase
    {
        private readonly ICoreService _service;

        public PM_CoreController(ICoreService service)
        {
            _service = service;
        }
         
        [HttpGet("{Deco},{Id_usuario}")]
        public async Task<IActionResultApi<IEnumerable<CoreDTO_Get>>> GetAll(string Deco, string Id_usuario, CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetAll(Deco, Id_usuario, cancellationToken);

                return new ActionResultApi<IEnumerable<CoreDTO_Get>>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<IEnumerable<CoreDTO_Get>>(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("{Deco}")]
        public async Task<IActionResultApi> Insert(string Deco, CoreDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Insert(Deco, dto, cancellationToken);

                if (respDB.StatusCode != 201)
                    return new ActionResultApi(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi(201, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{Id},{Deco}")]
        public async Task<IActionResultApi> Update(int Id , string Deco, CoreDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Update(Id, Deco, dto, cancellationToken);

                if (respDB.StatusCode != 202)
                    return new ActionResultApi(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi(202, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{Id},{Id_usuario}")]
        public async Task<IActionResultApi> Delete(int Id, string Id_usuario, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Delete(Id, Id_usuario, cancellationToken);

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
