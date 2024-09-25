using ApplicationClassLibrary.DTOs;
using ApplicationClassLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/core")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class CoreController : ControllerBase
    {
        private readonly ISecretService _secretService;

        public CoreController(ISecretService secretService)
        {
            _secretService = secretService;
        }
         
        [HttpGet("get-all/{sql_token},{id_usuario}")]
        public async Task<ActionResult<ResponseApiDTO<IEnumerable<SecretDTO>>>>  GetAll(string sql_token, string id_usuario, CancellationToken cancellationToken)
        {
            var response = await _secretService.GetAllAsync(sql_token, id_usuario, cancellationToken);

            if (response.StatusCode == 200)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-byid/{sql_token},{id_usuario},{id}")]
        public async Task<ActionResult<ResponseApiDTO<SecretDTO>>> GetById(string sql_token, string id_usuario, int id, CancellationToken cancellationToken)
        {
            var response = await _secretService.GetByIdAsync(sql_token, id_usuario, id, cancellationToken);

            if (response.StatusCode == 200)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("{sql_token}")]
        public async Task<ActionResult<ResponseApiDTO<SecretDTO>>> Insert(string sql_token, SecretDTO secretDTO, CancellationToken cancellationToken)
        {
            var response = await _secretService.InsertAsync(sql_token, secretDTO, cancellationToken);

            if (response.StatusCode == 201)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{sql_token}")]
        public async Task<ActionResult<ResponseApiDTO<SecretDTO>>> Update(string sql_token, SecretDTO secretDTO, CancellationToken cancellationToken)
        {
            var response = await _secretService.UpdateAsync(sql_token, secretDTO, cancellationToken);

            if (response.StatusCode == 202)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{sql_token},{id_usuario},{id}")]
        public async Task<ActionResult<ResponseApiDTO<SecretDTO>>> Delete(string sql_token, string id_usuario, int id, CancellationToken cancellationToken)
        {
            var response = await _secretService.DeleteAsync(sql_token, id_usuario, id, cancellationToken);

            if (response.StatusCode == 202)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }
    }
}
