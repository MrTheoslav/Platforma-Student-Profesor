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
    public class AssigmentController: ControllerBase
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



    }
}
