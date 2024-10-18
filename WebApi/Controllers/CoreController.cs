using ClassLibraryApplication.Interfaces;
using ClassLibraryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/core")]
    [ApiController]
    [Authorize]
    public class CoreController : ControllerBase
    {
        private readonly ICoreService _coreService;

        public CoreController(ICoreService coreService)
        {
            _coreService = coreService;
        }

        [HttpPatch("register")]
        public async Task<ActionResult<ResponseApiDTO<object>>> Register(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.RegisterAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("login")]
        public async Task<ActionResult<ResponseApiDTO<CoreIVDTO>>> Login(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.LoginAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("get-all")]
        public async Task<ActionResult<ResponseApiDTO<IEnumerable<CoreDTO>>>> GetAll(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.GetAllAsync(request.Sql_Token, request.Id_Usuario, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("get-byid")]
        public async Task<ActionResult<ResponseApiDTO<CoreDTO>>> GetById(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            if (request.Id.HasValue)
                return BadRequest("Id is required");

            var response = await _coreService.GetByIdAsync(request.Sql_Token, request.Id_Usuario, request.Id.Value, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("insert")]
        public async Task<ActionResult<ResponseApiDTO<CoreDTO>>> Insert(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken)
        {
            if (request.CoreData == null)
                return BadRequest("Core data is required");

            var response = await _coreService.InsertAsync(request.Sql_Token, request.CoreData, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("update")]
        public async Task<ActionResult<ResponseApiDTO<CoreDTO>>> Update(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken)
        {
            if (request.CoreData == null)
                return BadRequest("Core data is required");

            var response = await _coreService.UpdateAsync(request.Sql_Token, request.CoreData, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("delete")]
        public async Task<ActionResult<ResponseApiDTO<object>>> Delete(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            if (request.Id.HasValue)
                return BadRequest("Id is required");

            var response = await _coreService.DeleteAsync(request.Sql_Token, request.Id_Usuario, request.Id.Value, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
