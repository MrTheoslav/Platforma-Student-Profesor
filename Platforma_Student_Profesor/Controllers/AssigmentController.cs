using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Models;

namespace API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("API/[controller]")]
    public class AssigmentController : ControllerBase
    {
        private readonly IAssigmentService _assigmentService;
        private readonly IMapper _mapper;
        public AssigmentController(IAssigmentService assigmentService, IMapper mapper)
        {
            _assigmentService = assigmentService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpPost("AddAssigment")]
        public IActionResult AddAssignment(AssignmentDTO assignment)
        {
            if (assignment == null)
                return BadRequest("Zadanie nie może być puste.");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak.");

            if (_assigmentService.AssignmentExists(assignment.Name))
                return BadRequest("Zadanie już istnieje.");

            var assignmentMap = _mapper.Map<Assignment>(assignment);

            if (!_assigmentService.AddAssignment(assignmentMap))
                return BadRequest("Coś poszło nie tak podczas tworzenia zadania.");

            return Ok("Zadanie zostało poprawnie utworzone.");
        }


        [Authorize(Roles = "admin,teacher")]
        [HttpPut("updateAssigment")]
        public IActionResult UpdateAssignment(AssignmentDTO assignmentUpdate)
        {
            if (assignmentUpdate == null)
                return BadRequest("Zadanie do zmiany nie może być puste");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_assigmentService.AssignmentExists(assignmentUpdate.AssignmentID))
                return BadRequest("Nie istenieje takie zadanie");

            //CreateBy must be same as before!!!!
            var assignmentMap = _mapper.Map<Assignment>(assignmentUpdate);


            if (!_assigmentService.UpdateAssignment(assignmentMap))
                return BadRequest("Coś poszło nie tak podczas zmiany");

            return Ok("Zaktualizowano zadanie");
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpDelete("deleteAssigment")]
        public IActionResult DeleteAssignment(AssignmentDTO assignment)
        {
            if (assignment == null)
                return BadRequest("Zadanie do usunięcia nie może być puste");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_assigmentService.AssignmentExists(assignment.AssignmentID))
                return BadRequest("Nie istenieje takie zadanie");

            //CreateBy must be same as before!!!!
            var assignmentMap = _mapper.Map<Assignment>(assignment);


            if (!_assigmentService.DeleteAssignment(assignmentMap))
                return BadRequest("Coś poszło nie tak podczas usuwania");

            return Ok("Usunięto zadanie");
        }

        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("assignmentForRepository/{repositoryID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Assignment>))]
        [ProducesResponseType(400)]

        public IActionResult GetAssignmentForRepository(int repositoryID)
        {
            var assignmentDTOs = _mapper.Map<List<AssignmentDTO>>(_assigmentService.GetAssignmentsForRepository(repositoryID));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(assignmentDTOs);
        }

        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("assigmnentByID/{assigmnentID}")]
        [ProducesResponseType(200, Type = typeof(Assignment))]
        [ProducesResponseType(400)]
        public IActionResult GetAssignmentByID(int assigmnentID)
        {
            if (!_assigmentService.AssignmentExists(assigmnentID))
                return NotFound("Nie znaleziono zadania");

            var assignmentDTO = _mapper.Map<AssignmentDTO>(_assigmentService.GetAssignmentByID(assigmnentID));
            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            return Ok(assignmentDTO);
        }
        [Authorize(Roles = "admin,teacher,student")]
        [HttpGet("getMarkAndComment/{assigmnentID}/{userID}")]
        [ProducesResponseType(200, Type = typeof(UserAssigmnent))]
        [ProducesResponseType(400)]
        public IActionResult GetMarkAndComment(int assigmnentID, int userID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            if (!_assigmentService.UserAssigmnentExists(assigmnentID, userID))
            {
                return NotFound("Nie znaleziono oceny");
            }

            var userAssignment = _mapper.Map<UserAssigmnentDTO>(_assigmentService.GetUserAssigmnent(assigmnentID, userID));

            return Ok(userAssignment);
        }
        [Authorize(Roles = "admin,teacher")]
        [HttpGet("getUserAssignments/{assigmnentID}")]
        [ProducesResponseType(200, Type = typeof(UserAssigmnent))]
        [ProducesResponseType(400)]
        public IActionResult GetUserAssignments(int assigmnentID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            if (!_assigmentService.UserAssigmnentExists(assigmnentID))
            {
                return NotFound("Nie znaleziono połączenia");
            }
            var userAssignments = _mapper.Map<List<UserAssigmnentDTO>>(_assigmentService.GetUserAssigmnents(assigmnentID));

            return Ok(userAssignments);
        }
        [Authorize(Roles = "admin,teacher")]
        [HttpPost("CommentAndMark")]
        public IActionResult CommentAndMark(UserAssigmnentDTO userAssigmnentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            var ua = _mapper.Map<UserAssigmnent>(userAssigmnentDTO);

            if (!_assigmentService.CommentOrMark(ua))
                return BadRequest("Nie zapisano komentarza ani oceny");

            return Ok("Utworzono komentarz oraz ocenę");
        }

       

    }
}
