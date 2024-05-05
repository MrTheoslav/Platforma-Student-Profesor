using API.Interfaces;
using DAL;
using MODEL.Models;

namespace API.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly DataContext _context;

        public RepositoryService(DataContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateRepository(Repository repository)
        {
            var saved = _context.Add(repository);
            return Save();
        }

        public bool UpdateRepository(Repository repository)
        {
            var saved = _context.Update(repository);
            return Save();
        }

        public bool DeleteRepository(Repository repository)
        {
            _context.Remove(repository);
            return Save();
        }

        public ICollection<Repository> GetAllRepository()
        {
            return _context.Repository.OrderBy(r => r.Name).ToList();
        }

        public Repository GetRepositoryByID(int id)
        {
            return _context.Repository.Where(r => r.RepositoryID == id).FirstOrDefault();
        }

        public bool RepositoryExist(int id)
        {
            return _context.Repository.Any(r => r.RepositoryID == id);
        }

        public bool DeleteRepositoryForUser(List<Repository> repositories)
        {
            _context.RemoveRange(repositories);
            return Save();
        }
    }
}
