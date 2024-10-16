using ClassLibraryApplication.Interfaces;
using ClassLibraryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/technology")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class TechnologyController : ControllerBase
    {
        private readonly IBaseCRUDService<TechnologyDTO> _service;

        public TechnologyController(IBaseCRUDService<TechnologyDTO> service)
        {
            _service = service;
        }


    }
}
