using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IMapper _mapper;
        public RepositoryController(IRepositoryService repositoryService, IMapper mapper)
        {
            _repositoryService = repositoryService;
            _mapper = mapper;
        }


        [HttpGet("allRepository")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Repository>))]
        [ProducesResponseType(400)]
        public IActionResult GetRepository()
        {
            var repositoryDTOs = _mapper.Map<List<RepositoryDTO>>(_repositoryService.GetAllRepository());

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(repositoryDTOs);
        }


        [HttpGet("repositoryById/{repositoryID}")]
        [ProducesResponseType(200, Type = typeof(Repository))]
        [ProducesResponseType(400)]
        public IActionResult GetRepositoryById(int repositoryId)
        {

            if(_repositoryService.RepositoryExist(repositoryId))
                return NotFound();

            var repositoryDTO = _mapper.Map<RepositoryDTO>(_repositoryService.GetRepositoryByID(repositoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(repositoryDTO);
        }



     /*   [HttpPost("{userID}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateRepository(int userID, [FromBody] RepositoryDTO repositoryCreate)
        {
            if (repositoryCreate == null)
                return BadRequest("Repozytorium nie może być puste");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            var repositoryMap = _mapper.Map<Repository>(repositoryCreate);

          

            if (!_repositoryService.CreateRepository(repositoryMap))
            {
                return BadRequest("Coś poszło nie tak podczas tworzenia");
            }

            return Ok("Pomyślnie utworzono repozytorium");
        }*/

    }
}
