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
        ICollection<Assignment> GetAssignmentsForRepository(int repositoryID);
        Assignment GetAssignmentByID(int id);
        bool UserAssigmnentExists(int assignmentID, int userID);
        bool UserAssigmnentExists(int assignmentID);
        UserAssigmnent GetUserAssigmnent(int assignmentID, int userID);
        bool CommentOrMark(UserAssigmnent userAssigmnent);
        ICollection<UserAssigmnent> GetUserAssigmnents(int assignmentID);
        bool removeFileForUSer(int userID);
    }
}
