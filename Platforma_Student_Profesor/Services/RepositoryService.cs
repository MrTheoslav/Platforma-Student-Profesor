using API.Authorization;
using API.Interfaces;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MODEL.Models;
using System.Security.Claims;

namespace API.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly DataContext _context;
        private readonly IAuthorizationService _authorizationService;

        public RepositoryService(DataContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateRepository(Repository repository, int userID)
        {
            
            repository.CreatedById = userID;
           _context.Add(repository);
            var saveRepo = Save();

            var userRepository = new UserRepository()
            {
                EnterDate = DateTime.Now,
                Privilage = 2,
                IsMember = true,
                UserID = userID,
                RepositoryID = repository.RepositoryID
            };

            _context.Add(userRepository);
            var saveUserRepo = Save();

            if (saveUserRepo && saveRepo)
            {
                return true;
            }
            else
            {
                return false;
            }
             
        }

        public bool UpdateRepository(Repository repository, ClaimsPrincipal user)
        {


            var authorizationResult = _authorizationService.AuthorizeAsync(user, repository, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                return false;
            }

            var saved = _context.Update(repository);
            return Save();
        }

        public bool DeleteRepository(Repository repository,ClaimsPrincipal user, int userId)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(user, repository, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            var userRepositoryToDelate = _context.UsersRepository.Where(x => x.UserID == userId).Where(u => u.RepositoryID == repository.RepositoryID).FirstOrDefault();
            _context.Remove(repository);
            _context.Remove(userRepositoryToDelate);
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

        public bool RepositoryExistByName(string name)
        {
            return _context.Repository.Any(r => r.Name.Trim().ToLower() == name.Trim().ToLower());
        }


        public bool DeleteRepositoryForUser(List<Repository> repositories)
        {
            _context.RemoveRange(repositories);

     
            return Save();
        }

       
    }
}
