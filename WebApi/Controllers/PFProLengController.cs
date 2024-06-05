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
    [Route("api/ProLeng")]
    [ApiController]
    public class PFProLengController : ControllerBase
    {
        private readonly ISingleService<ProLengDTO> _service;

        public PFProLengController(ISingleService<ProLengDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResultApi<IEnumerable<ProLengDTO>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetAll(cancellationToken);

                return new ActionResultApi<IEnumerable<ProLengDTO>>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<IEnumerable<ProLengDTO>>(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResultApi<ProLengDTO>> Insert(ProLengDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Insert(dto, cancellationToken);

                if (respDB.StatusCode != 201)
                    return new ActionResultApi<ProLengDTO>(respDB.StatusCode, respDB.Msge);

                return new ActionResultApi<ProLengDTO>(201, respDB.Msge);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<ProLengDTO>(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResultApi> Delete(ProLengDTO dto, CancellationToken cancellationToken)
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
