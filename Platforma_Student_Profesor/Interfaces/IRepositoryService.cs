using MODEL.Models;

namespace API.Interfaces
{
    public interface IRepositoryService
    {
        bool CreateRepository(Repository repository);
        bool DeleteRepository(Repository repository);
        bool DeleteRepositoryForUser(List<Repository> repositories);
        ICollection<Repository> GetAllRepository();
        Repository GetRepositoryByID(int id);
        bool RepositoryExist(int id);
        bool Save();
        bool UpdateRepository(Repository repository);
    }
}