
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Models;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }


  
        [HttpGet("AllRole")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [ProducesResponseType(400)]
        public IActionResult GetRole()
        {
            var roleDTOs = _mapper.Map<List<RoleDTO>>(_roleService.GetAllRoles());

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(roleDTOs);
        }


        [HttpGet("{roleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult GetRoleById(int roleId)
        {

            if (!_roleService.RoleExist(roleId))
                return NotFound();

            var roleDTO = _mapper.Map<RoleDTO>(_roleService.GetRoleByID(roleId));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(roleDTO);
        }


   
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateRole([FromBody] RoleDTO roleCreate)
        {
            if (roleCreate == null)
                return BadRequest("Rola nie może być pusta");

            //zrobić sprawdzenie czy juz istnieje

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            var roleMap = _mapper.Map<Role>(roleCreate);



            if (!_roleService.CreateRole(roleMap))
            {
                return BadRequest("Coś poszło nie tak podczas tworzenia");
            }

            return Ok("Pomyślnie utworzono role");
        }


        [HttpPut("updateRole")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateRole([FromBody] RoleDTO roleUpdate)
        {
            if (roleUpdate == null)
                return BadRequest("Rola jest pusta");

            if (!_roleService.RoleExist(roleUpdate.RoleID))
                return BadRequest("Rola nie istnieje");

            var roleMap = _mapper.Map<Role>(roleUpdate);

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (_roleService.UpdateRole(roleMap))
                return BadRequest("Coś poszło nie tak podczas aktualizacji roli");

            return Ok("Rola zaktualizowana");

        }
      
        [HttpDelete("deleteRole/{roleID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRole(int roleID)
        {
            if (_roleService.RoleExist(roleID))
                return NotFound("Nie znaleziono roli");

            var roleToDelete = _roleService.GetRoleByID(roleID);

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_roleService.DeleteRole(roleToDelete))
            {
                return BadRequest("Coś poszło nie tak podczas usuwania roli");
            }

            return Ok("Pomyślnie usunięto role");
        }

    }
}
