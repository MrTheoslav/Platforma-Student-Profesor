using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    [AllowAnonymous]
    public class AccountController:ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUSer([FromBody] RegisterUserDto registerUserDto)
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
    }
}
