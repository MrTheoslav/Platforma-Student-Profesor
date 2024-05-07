using MODEL.Models;
using System.Security.Claims;

namespace API.Interfaces
{
    public interface IRepositoryService
    {
        bool CreateRepository(Repository repository, int userID);
        bool DeleteRepository(Repository repository, ClaimsPrincipal user, int userId);
        bool DeleteRepositoryForUser(List<Repository> repositories);
        ICollection<Repository> GetAllRepository();
        Repository GetRepositoryByID(int id);
        bool RepositoryExist(int id);
        bool Save();
        bool UpdateRepository(Repository repository, ClaimsPrincipal user);
        bool RepositoryExistByName(string name);
    }
}