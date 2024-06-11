using MODEL.Models;
using System.Security.Claims;

namespace API.Interfaces
{
    public interface IRepositoryService
    {
        bool CreateRepository(Repository repository);
        bool DeleteRepository(Repository repository);
        bool DeleteRepositoryForUser(List<Repository> repositories);
        ICollection<Repository> GetAllRepository();
        Repository GetRepositoryByID(int id);
        bool RepositoryExists(int id);
        bool Save();
        bool UpdateRepository(Repository repository);
        bool RepositoryExistByName(string name);
        bool UserExistsInRepository(int userID, int repositoryID);
        bool RemoveStudentFromRepository(UserRepository userRepository);
        bool AddStudentToRepository(UserRepository userRepository);
        bool AddAssignment(Assignment assignment);
        bool AddStudentToAssignment(UserAssigmnent userAssignment);
        bool AssignmentExists(string name);
        bool AssignmentExists(int id);
        bool UpdateAssignment(Assignment assignment);
        bool DeleteAssignment(Assignment assignment);

    }
}