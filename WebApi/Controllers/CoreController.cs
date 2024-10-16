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
        private readonly ICoreService _secretService;

        public CoreController(ICoreService secretService)
        {
            _secretService = secretService;
        }
         
        [HttpGet("get-all/{sql_token},{id_usuario}")]
        public async Task<ActionResult<ResponseApiDTO<IEnumerable<CoreDTO>>>>  GetAll(string sql_token, string id_usuario, CancellationToken cancellationToken)
        {
            var response = await _secretService.GetAllAsync(sql_token, id_usuario, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-byid/{sql_token},{id_usuario},{id}")]
        public async Task<ActionResult<ResponseApiDTO<CoreDTO>>> GetById(string sql_token, string id_usuario, int id, CancellationToken cancellationToken)
        {
            var response = await _secretService.GetByIdAsync(sql_token, id_usuario, id, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("{sql_token}")]
        public async Task<ActionResult<ResponseApiDTO<CoreDTO>>> Insert(string sql_token, CoreDTO secretDTO, CancellationToken cancellationToken)
        {
            var response = await _secretService.InsertAsync(sql_token, secretDTO, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{sql_token}")]
        public async Task<ActionResult<ResponseApiDTO<CoreDTO>>> Update(string sql_token, CoreDTO secretDTO, CancellationToken cancellationToken)
        {
            var response = await _secretService.UpdateAsync(sql_token, secretDTO, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{sql_token},{id_usuario},{id}")]
        public async Task<ActionResult<ResponseApiDTO<object>>> Delete(string sql_token, string id_usuario, int id, CancellationToken cancellationToken)
        {
            var response = await _secretService.DeleteAsync(sql_token, id_usuario, id, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
