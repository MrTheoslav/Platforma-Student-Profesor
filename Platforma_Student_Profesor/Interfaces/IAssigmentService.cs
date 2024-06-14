using MODEL.Models;

namespace API.Interfaces
{
    public interface IAssigmentService
    {

        bool AddAssignment(Assignment assignment);
        bool AddStudentToAssignment(UserAssigmnent userAssignment);
        bool AssignmentExists(string name);
        bool AssignmentExists(int id);
        bool UpdateAssignment(Assignment assignment);
        bool DeleteAssignment(Assignment assignment);
    }
}
