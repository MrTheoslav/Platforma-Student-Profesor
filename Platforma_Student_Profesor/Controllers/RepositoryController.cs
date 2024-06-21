
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Models;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
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
        private readonly IUserContextService _userContextService;
        private readonly IAccountService _accountService;
        public RepositoryController(IRepositoryService repositoryService, IMapper mapper, IUserContextService userContextService, IAccountService accountService)
        {
            _repositoryService = repositoryService;
            _mapper = mapper;
            _userContextService = userContextService;
            _accountService = accountService;
        }

        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("allRepository")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Repository>))]
        [ProducesResponseType(400)]


        public IActionResult GetAllRepository()
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
        [ProducesResponseType(200, Type = typeof(RepositoryDTO))]
        [ProducesResponseType(400)]


        public IActionResult GetRepositoryById(int repositoryID)
        {

            if (!_repositoryService.RepositoryExists(repositoryID))
                return NotFound();

            var repositoryDTO = _mapper.Map<RepositoryDTO>(_repositoryService.GetRepositoryByID(repositoryID));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(repositoryDTO);
        }
        

            [Authorize(Roles = "admin,teacher")]
            [HttpPost("createRepository")]
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
        [HttpPut("updateRepository")]
        public IActionResult UpdateRepository([FromBody] RepositoryDTO repositoryUpdate)
        {
            if (repositoryUpdate == null)
                return BadRequest("Repozytorium do zmiany nie może być puste");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_repositoryService.RepositoryExists(repositoryUpdate.RepositoryID))
                return BadRequest("Nie istenieje takie repozytorium");

            //CreateBy must be same as before!!!!
            var repositoryMap = _mapper.Map<Repository>(repositoryUpdate);


            if (!_repositoryService.UpdateRepository(repositoryMap))
                return BadRequest("Coś poszło nie tak podczas zmiany");

            return Ok("Zaktualizowano repozytorium");
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpDelete("deleteRepository")]
        public IActionResult DeleteRepository(RepositoryDTO repositoryDTO)
        {
            if (repositoryDTO == null)
                return BadRequest("repozytorium do usunięcia nie może być puste");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_repositoryService.RepositoryExists(repositoryDTO.RepositoryID))
                return BadRequest("Nie istenieje takie repozytorium");

            //CreateBy must be same as before!!!!
            var repositoryMap = _mapper.Map<Repository>(repositoryDTO);


            if (!_repositoryService.DeleteRepository(repositoryMap))
                return BadRequest("Coś poszło nie tak podczas usuwania");

            return Ok("Usunięto repozytorium");
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpDelete("removeStudentFromRepository/{repoID}/{studentID}")]
        public IActionResult RemoveStudentFromRepository(int repoID, int studentID)
        {
        

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak.");

            if (!_repositoryService.UserExistsInRepository(repoID, studentID))
                return NotFound("Dane repozytorium nie posiada tego ucznia.");

            var userRepository = _repositoryService.GetUserRepository(repoID, studentID);


            if (!_repositoryService.RemoveStudentFromRepository(userRepository))
                return BadRequest("Coś poszło nie tak podczas usuwania ucznia z repozytorium.");

            return Ok("Usunięto ucznia z repozytorium");
        }

        
        [Authorize(Roles = "admin,teacher, student")]
        [HttpPost("addStudentToRepository")]
        public IActionResult AddStudentToRepository([FromBody] UserRepositoryDTO userRepository)
        {
            if (userRepository == null)
                return BadRequest("Nie można dodać pustego ucznia.");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak.");
            userRepository.UserID = (int)_userContextService.GetUserId;
            userRepository.EnterDate = DateTime.Now;

            if (_repositoryService.UserExistsInRepository(userRepository.UserID, userRepository.RepositoryID))
                return BadRequest("Użytkownik już istnieje w tym repozytorium.");

            var userRepositoryMap = new UserRepository()
            {
                UserID = userRepository.UserID,
                RepositoryID = userRepository.RepositoryID,
                IsMember = userRepository.IsMember,
                EnterDate = DateTime.Now,
                Privilage = 2
             
            };

            if (!_repositoryService.AddStudentToRepository(userRepositoryMap))
                return BadRequest("Coś poszło nie tak podczas dodawania ucznia do repozytorium.");

            return Ok("Dodano ucznia do repozytorium");
        }
        
        

        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("repositoryForUser")]
        [ProducesResponseType(200, Type = typeof(RepositoryDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetRepositoryForUser()
        {
           
            var repositoryDTOs = _mapper.Map<List<RepositoryDTO>>(_repositoryService.GetRepositoryForUser((int)_userContextService.GetUserId));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(repositoryDTOs);


        }

        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("UserExistInRepo/{repoID}")]
        [ProducesResponseType(200, Type = typeof(UserRepositoryDTO))]
        [ProducesResponseType(400)]
        public IActionResult UserExistInRepository(int repoID)
        {
            var userRepo = _repositoryService.UserConfirmAndExist((int)_userContextService.GetUserId, repoID);
            var userRepositoryDTO = new UserRepositoryDTO();

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }


            if (userRepo.Privilage == 0)
            {
                userRepositoryDTO = new UserRepositoryDTO()
                {
                    UserID = 0,
                    EnterDate = DateTime.Now,
                    HasPrivilage = false,
                    IsMember = false,
                    RepositoryID = 0
                };
                return Ok(userRepositoryDTO);
            }
            else
            {
                  userRepositoryDTO = new UserRepositoryDTO()
                {
                    UserID = userRepo.UserID,
                    EnterDate = userRepo.EnterDate,
                    HasPrivilage = false,
                    IsMember = userRepo.IsMember,
                    RepositoryID = userRepo.RepositoryID
                };

                    return Ok(userRepositoryDTO);
            }



            return BadRequest("Coś poszło nie tak");


        }

        [Authorize(Roles = "admin,teacher")]
        [HttpGet("accountToConfirm/{repositoryID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetAccountsToConfirm(int repositoryID)
        {
            var userRepositories = _repositoryService.GetStudentsToConfirm(repositoryID);
           ICollection<UserDTO> userDTOs = new List<UserDTO>();

            foreach (var userRepositoryDTO in userRepositories)
            {
                var userDto = _mapper.Map<UserDTO>(_accountService.GetUserById(userRepositoryDTO.UserID));
                userDTOs.Add(userDto);
            }
           
            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(userDTOs);
        }


        [Authorize(Roles = "admin,teacher")]
        [HttpPost("confirmStudent/{userID}/{repositoryID}")]
        public IActionResult ConfirmUser(int userID, int repositoryID)
        {
            bool decision = _repositoryService.ConfirmUser(userID, repositoryID);

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            if (decision)
            {
                return Ok("Użytkownik zatwierdzony");

            }
            else
            {
                return BadRequest("Nie udało sie zatwierdzić użytkownika");
            }

        }

        [Authorize(Roles = "admin,teacher")]
        [HttpGet("acceptedStudentinRepository/{repositoryID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserRepositoryDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetAcceptedStudentsForRepository(int repositoryID)
        {
            var userRepositories = _repositoryService.GetAcceptedStudents(repositoryID);
            ICollection<UserDTO> userDTOs = new List<UserDTO>();

            foreach (var userRepositoryDTO in userRepositories)
            {
                var userDto = _mapper.Map<UserDTO>(_accountService.GetUserById(userRepositoryDTO.UserID));
                userDTOs.Add(userDto);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(userDTOs);
            
        }

    }
}
