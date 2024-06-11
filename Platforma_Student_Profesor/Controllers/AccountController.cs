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
    [AllowAnonymous]
    public class AccountController:ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            if (token.Equals("userIsNull"))
            {
                return BadRequest("Incorrect password or emial");
            }
            else if (token.Equals("PasswordNotCorrect"))
            {
                return BadRequest("Incorrect password or emial");
            }
            else
            {
                return Ok(token);
            }
        }

        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("getUser")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetUser()
        {

            var user = _mapper.Map<UserDTO>(_accountService.GetUser());

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }


            if(user == null)
            {
                return BadRequest("Coś poszło nie tak");
            }

            return Ok(user);
        }
    }
}
