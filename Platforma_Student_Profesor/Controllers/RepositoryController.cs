﻿
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Models;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "admin,teacher,student")]
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


        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("repositoryById/{repositoryID}")]
        [ProducesResponseType(200, Type = typeof(Repository))]
        [ProducesResponseType(400)]
        public IActionResult GetRepositoryById(int repositoryID)
        {

            if (!_repositoryService.RepositoryExist(repositoryID))
                return NotFound();

            var repositoryDTO = _mapper.Map<RepositoryDTO>(_repositoryService.GetRepositoryByID(repositoryID));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(repositoryDTO);
        }


        [Authorize(Roles = "admin,teacher")]
        [HttpPost]
           [ProducesResponseType(201)]
           [ProducesResponseType(400)]
           public IActionResult CreateRepository([FromBody] RepositoryDTO repositoryCreate)
           {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

               if (repositoryCreate == null)
                   return BadRequest("Repozytorium nie może być puste");

               if (!ModelState.IsValid)
                   return BadRequest("Coś poszło nie tak");

               if(_repositoryService.RepositoryExistByName(repositoryCreate.Name))
                return BadRequest("Istnieje repozytorium o takiej samej nazwie");

                var repositoryMap = _mapper.Map<Repository>(repositoryCreate);
           


               if (!_repositoryService.CreateRepository(repositoryMap))
               {
                   return BadRequest("Coś poszło nie tak podczas tworzenia");
               }

               return Ok("Pomyślnie utworzono repozytorium");
           }
        [Authorize(Roles = "admin,teacher")]
        [HttpPut]
        public IActionResult UpdateRepository([FromBody] RepositoryDTO repositoryUpdate)
        {
            if (repositoryUpdate == null)
                return BadRequest("Repozytorium do zmiany nie może być puste");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_repositoryService.RepositoryExist(repositoryUpdate.RepositoryID))
                return BadRequest("Nie istenieje takie repozytorium");

            //CreateBy must be same as before!!!!
            var repositoryMap = _mapper.Map<Repository>(repositoryUpdate);


            if (!_repositoryService.UpdateRepository(repositoryMap))
                return BadRequest("Coś poszło nie tak podczas zmiany");

            return Ok("Zaktualizowano repozytorium");
        }



    }
}
