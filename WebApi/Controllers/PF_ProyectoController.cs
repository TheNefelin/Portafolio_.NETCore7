using BibliotecaPortafolio.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Utils;

namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
 
    //[Route("api/Proyecto")]
    //[ApiController]
    ////[Authorize(Roles = $"{UserRole.ADMIN}, {UserRole.USER}")]
    //public class PF_ProyectoController : ControllerBase
    //{
    //    private readonly IBaseService<ProyectoDTO_Get, ProyectoDTO_PostPut> _service;

    //    public PF_ProyectoController(IBaseService<ProyectoDTO_Get, ProyectoDTO_PostPut> service)
    //    {
    //        _service = service;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResultApi<IEnumerable<ProyectoDTO_Get>>> GetAll(CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            var respData = await _service.GetAll(cancellationToken);

    //            return new ActionResultApi<IEnumerable<ProyectoDTO_Get>>(200, "OK", respData);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new ActionResultApi<IEnumerable<ProyectoDTO_Get>>(500, $"Error: {ex.Message}");
    //        }
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<IActionResultApi<ProyectoDTO_Get>> GetById(int id, CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            var respData = await _service.GetById(id, cancellationToken);

    //            if (respData == null)
    //                return new ActionResultApi<ProyectoDTO_Get>(400, "El Id No Existe");

    //            return new ActionResultApi<ProyectoDTO_Get>(200, "OK", respData);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new ActionResultApi<ProyectoDTO_Get>(500, $"Error: {ex.Message}");
    //        }
    //    }

    //    //[Authorize(Roles = UserRole.ADMIN)]
    //    [HttpPost]
    //    public async Task<IActionResultApi<ProyectoDTO_Get>> Insert(ProyectoDTO_PostPut dto, CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            var respDB = await _service.Insert(dto, cancellationToken);

    //            if (respDB.StatusCode != 201)
    //                return new ActionResultApi<ProyectoDTO_Get>(respDB.StatusCode, respDB.Msge);

    //            var respData = await _service.GetById(respDB.Id, cancellationToken);
    //            return new ActionResultApi<ProyectoDTO_Get>(201, respDB.Msge, respData);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new ActionResultApi<ProyectoDTO_Get>(500, $"Error: {ex.Message}");
    //        }
    //    }

    //    //[Authorize(Roles = UserRole.ADMIN)]
    //    [HttpPut("{id}")]
    //    public async Task<IActionResultApi<ProyectoDTO_Get>> Update(int id, ProyectoDTO_PostPut dto, CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            var respDB = await _service.Update(id, dto, cancellationToken);

    //            if (respDB.StatusCode != 202)
    //                return new ActionResultApi<ProyectoDTO_Get>(respDB.StatusCode, respDB.Msge);

    //            var respData = await _service.GetById(respDB.Id, cancellationToken);
    //            return new ActionResultApi<ProyectoDTO_Get>(202, respDB.Msge, respData);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new ActionResultApi<ProyectoDTO_Get>(500, $"Error: {ex.Message}");
    //        }
    //    }

    //    //[Authorize(Roles = UserRole.ADMIN)]
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResultApi> Delete(int id, CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            var respDB = await _service.Delete(id, cancellationToken);

    //            if (respDB.StatusCode != 202)
    //                return new ActionResultApi(respDB.StatusCode, respDB.Msge);

    //            return new ActionResultApi(202, respDB.Msge);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new ActionResultApi(500, $"Error: {ex.Message}");
    //        }
    //    }
    //}
}
