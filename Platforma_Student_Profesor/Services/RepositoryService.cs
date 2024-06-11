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

        private readonly IUserContextService _userContextService;

        public RepositoryService(DataContext context, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateRepository(Repository repository)
        {

            repository.CreatedById = _userContextService.GetUserId;
            _context.Add(repository);
            var saveRepo = Save();

            var userRepository = new UserRepository()
            {
                EnterDate = DateTime.Now,
                Privilage = 2,
                IsMember = true,
                UserID = (int)_userContextService.GetUserId,
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

        public bool UpdateRepository(Repository repository)
        {


            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, repository, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            var saved = _context.Update(repository);
            return Save();
        }

        public bool DeleteRepository(Repository repository)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, repository, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            var userRepositoryToDelate = _context.UsersRepository.Where(x => x.UserID == _userContextService.GetUserId).Where(u => u.RepositoryID == repository.RepositoryID).FirstOrDefault();
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

        public bool RepositoryExists(int id)
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

        public bool UserExistsInRepository(int userID, int repositoryID)
        {
            return _context.UsersRepository.Any(ur => ur.UserID == userID && ur.RepositoryID == repositoryID);
        }

        public bool AddStudentToRepository(UserRepository userRepository)
        {
            _context.Add(userRepository);

            return Save();
        }

        public bool RemoveStudentFromRepository(UserRepository userRepository)
        {
            _context.Remove(userRepository);

            return Save();
        }

        public bool AssignmentExists(string name)
        {
            return _context.Assignments.Any(a => a.Name == name);
        }
        
        public bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(a => a.AssignmentID == id);
        }

        public bool AddAssignment(Assignment assignment)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, assignment, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            _context.Add(assignment);

            if (!Save())
                return false;

            foreach (UserRepository user in _context.UsersRepository)
            {
                if (user.RepositoryID == assignment.RepositoryID)
                {
                    var userAssignment = new UserAssigmnent
                    {
                        UserID = user.UserID,
                        AssigmnentID = assignment.AssignmentID
                    };

                    if (!AddStudentToAssignment(userAssignment))
                        return false;
                }
            }

            return true;
        }

        public bool AddStudentToAssignment(UserAssigmnent userAssignment)
        {
            _context.Add(userAssignment);

            return Save();
        }

        public bool UpdateAssignment(Assignment assignment)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, assignment, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            _context.Update(assignment);

            return Save();
        }

        public bool DeleteAssignment(Assignment assignment)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, assignment, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            _context.Remove(assignment);

            return Save();
        }
    }
}
