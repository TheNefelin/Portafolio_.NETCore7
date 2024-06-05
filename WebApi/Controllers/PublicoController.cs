using BibliotecaPortafolio.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicoController : ControllerBase
    {
        private readonly ILogger<PublicoController> _logger;
        private readonly IBaseService<EnlaceGrpDTO_Get, EnlaceGrpDTO_PostPut> _enlaceGrpService;
        private readonly IBaseService<EnlaceDTO_Get, EnlaceDTO_PostPut> _enlaceService;
        private readonly IBaseService<YoutubeDTO_Get, YoutubeDTO_PostPut> _youtubeService;
        private readonly IBaseService<ProyectoDTO_Get, ProyectoDTO_PostPut> _proyectoService;
        private readonly IBaseService<LenguajeDTO_Get, LenguajeDTO_PostPut> _lenguajeService;
        private readonly IBaseService<TecnologiaDTO_Get, TecnologiaDTO_PostPut> _tecnologiaService;
        private readonly ISingleService<ProLengDTO> _lpService;
        private readonly ISingleService<ProTecDTO> _tpService;

        public PublicoController(
            ILogger<PublicoController> logger,
            IBaseService<EnlaceGrpDTO_Get, EnlaceGrpDTO_PostPut> enlaceGrpService,
            IBaseService<EnlaceDTO_Get, EnlaceDTO_PostPut> enlaceService,
            IBaseService<YoutubeDTO_Get, YoutubeDTO_PostPut> youtubeService,
            IBaseService<ProyectoDTO_Get, ProyectoDTO_PostPut> proyectoService,
            IBaseService<LenguajeDTO_Get, LenguajeDTO_PostPut> lenguajeService,
            IBaseService<TecnologiaDTO_Get, TecnologiaDTO_PostPut> tecnologiaService,
            ISingleService<ProLengDTO> lpService,
            ISingleService<ProTecDTO> tpService)
        {
            _logger = logger;
            _enlaceGrpService = enlaceGrpService;
            _enlaceService = enlaceService;
            _youtubeService = youtubeService;
            _proyectoService = proyectoService;
            _lenguajeService = lenguajeService;
            _tecnologiaService = tecnologiaService;
            _lpService = lpService;
            _tpService = tpService;
        }

        [HttpGet]
        [Route("Enlace")]
        public async Task<IActionResultApi<IEnumerable<EnlaceDTO>>> GetAllEnlaces(CancellationToken cancellationToken)
        {
            try
            {
                var enlacesGrpsTask = _enlaceGrpService.GetAll(cancellationToken);
                var enlacesTask = _enlaceService.GetAll(cancellationToken);

                await Task.WhenAll(enlacesGrpsTask, enlacesTask);

                var enlacesGrps = await enlacesGrpsTask;
                var enlaces = await enlacesTask;

                var result = enlacesGrps.Select(grp => new EnlaceDTO
                {
                    Id = grp.Id,
                    Nombre = grp.Nombre,
                    Estado = grp.Estado,
                    Enlaces = enlaces.Where(e => e.Id_EnlaceGrp == grp.Id).ToList()
                }).ToList();

                return new ActionResultApi<IEnumerable<EnlaceDTO>>(200, "OK", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Route(\"Enlaces\")]");
                return new ActionResultApi<IEnumerable<EnlaceDTO>>(500, $"Error: {ex}");
            }
        }

        [HttpGet]
        [Route("Youtube")]
        public async Task<IActionResultApi<IEnumerable<YoutubeDTO_Get>>> GetAllYoutube(CancellationToken cancellationToken)
        {
            try
            {
                var respData = await _youtubeService.GetAll(cancellationToken);

                return new ActionResultApi<IEnumerable<YoutubeDTO_Get>>(200, "OK", respData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Route(\"Youtube\")]");
                return new ActionResultApi<IEnumerable<YoutubeDTO_Get>>(500, $"Error: {ex}");
            }
        }

        [HttpGet]
        [Route("Proyecto")]
        public async Task<IActionResultApi<IEnumerable<ProyectoDTO>>> GetAllProyecto(CancellationToken cancellationToken)
        {
            try
            {
                var proyectoTask = _proyectoService.GetAll(cancellationToken);
                var lpTask = _lpService.GetAll(cancellationToken);
                var lenguajeTask = _lenguajeService.GetAll(cancellationToken);
                var tpTask = _tpService.GetAll(cancellationToken);
                var tecnologiaTask = _tecnologiaService.GetAll(cancellationToken);
                
                await Task.WhenAll(proyectoTask, lpTask, lenguajeTask, tpTask, tecnologiaTask);

                var proyectos = await proyectoTask;
                var lps = await lpTask;
                var lenguajes = await lenguajeTask;
                var tps = await tpTask;
                var tecnologias = await tecnologiaTask;

                var result = proyectos.Select(pro => new ProyectoDTO
                {
                    Id = pro.Id,
                    Nombre = pro.Nombre,
                    ImgUrl = pro.ImgUrl,
                    Lenguajes = lps.Where(lp => lp.Id_Proyecto == pro.Id)
                                   .Select(lp => lenguajes.FirstOrDefault(l => l.Id == lp.Id_Lenguaje) ?? new LenguajeDTO_Get())
                                   .Where(l => l != null).ToList(),
                    Tecnologias = tps.Where(tp => tp.Id_Proyecto == pro.Id)
                                     .Select(tp => tecnologias.FirstOrDefault(t => t.Id == tp.Id_Tecnologia) ?? new TecnologiaDTO_Get())
                                     .Where(t => t != null).ToList()
                }).ToList();

                return new ActionResultApi<IEnumerable<ProyectoDTO>>(200, "OK", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Route(\"Proyecto\")]");
                return new ActionResultApi<IEnumerable<ProyectoDTO>>(500, $"Error: {ex}");
            }
        }
    }
}
