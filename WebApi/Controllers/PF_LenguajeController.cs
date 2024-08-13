using BibliotecaAuth.Classes;
using BibliotecaPortafolio.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Route("api/lenguaje")]
    [ApiController]
    [Authorize(Roles = $"{UserRole.ADMIN}, {UserRole.USER}")]
    public class PF_LenguajeController : ControllerBase
    {
        private readonly IBaseService<LenguajeDTO_Get, LenguajeDTO_PostPut> _service;

        public PF_LenguajeController(IBaseService<LenguajeDTO_Get, LenguajeDTO_PostPut> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResultApi<IEnumerable<LenguajeDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetAll(cancellationToken);

                return new ActionResultApi<IEnumerable<LenguajeDTO_Get>>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<IEnumerable<LenguajeDTO_Get>>(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResultApi<LenguajeDTO_Get>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _service.GetById(id, cancellationToken);

                if (respData == null)
                    return new ActionResultApi<LenguajeDTO_Get>(400, "El Id No Existe");

                return new ActionResultApi<LenguajeDTO_Get>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<LenguajeDTO_Get>(500, $"Error: {ex.Message}");
            }
        }

        [Authorize(Roles = UserRole.ADMIN)]
        [HttpPost]
        public async Task<IActionResultApi<LenguajeDTO_Get>> Insert(LenguajeDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Insert(dto, cancellationToken);

                if (respDB.StatusCode != 201)
                    return new ActionResultApi<LenguajeDTO_Get>(respDB.StatusCode, respDB.Msge);

                var respData = await _service.GetById(respDB.Id, cancellationToken);
                return new ActionResultApi<LenguajeDTO_Get>(201, respDB.Msge, respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<LenguajeDTO_Get>(500, $"Error: {ex.Message}");
            }
        }

        [Authorize(Roles = UserRole.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResultApi<LenguajeDTO_Get>> Update(int id, LenguajeDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Update(id, dto, cancellationToken);

                if (respDB.StatusCode != 202)
                    return new ActionResultApi<LenguajeDTO_Get>(respDB.StatusCode, respDB.Msge);

                var respData = await _service.GetById(respDB.Id, cancellationToken);
                return new ActionResultApi<LenguajeDTO_Get>(202, respDB.Msge, respData);
            }
            catch (Exception ex)
            {
                return new ActionResultApi<LenguajeDTO_Get>(500, $"Error: {ex.Message}");
            }
        }

        [Authorize(Roles = UserRole.ADMIN)]
        [HttpDelete("{id}")]
        public async Task<IActionResultApi> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var respDB = await _service.Delete(id, cancellationToken);

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
