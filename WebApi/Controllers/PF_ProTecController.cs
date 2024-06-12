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
    [Route("api/ProTec")]
    [ApiController]
    public class PF_ProTecController : ControllerBase
    {
        private readonly ISingleService<ProTecDTO> _service;

        public PF_ProTecController(ISingleService<ProTecDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResultApi<IEnumerable<ProTecDTO>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetAll(cancellationToken);

                return new ActionResultApi<IEnumerable<ProTecDTO>>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<IEnumerable<ProTecDTO>>(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResultApi<ProTecDTO>> Insert(ProTecDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Insert(dto, cancellationToken);

                if (respDB.StatusCode != 201)
                    return new ActionResultApi<ProTecDTO>(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi<ProTecDTO>(201, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<ProTecDTO>(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResultApi> Delete(ProTecDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Delete(dto, cancellationToken);

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
