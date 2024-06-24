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
        private readonly IAssigmentService _assigmentService;

        public RepositoryService(DataContext context, IAuthorizationService authorizationService, IUserContextService userContextService, IAssigmentService assigmentService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _assigmentService = assigmentService;   
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

            if(!authorizationResult.Succeeded)
            {
                return false;
            }

            var saved = _context.Update(repository);
            return Save();
        }

        public bool DeleteRepository(Repository repository)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, repository, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            var userRole = _context.Users.Where(u => u.UserID == (int)_userContextService.GetUserId).FirstOrDefault();




            var assigmentForRepository = _assigmentService.GetAssignmentsForRepository(repository.RepositoryID);

            foreach( var assigment in assigmentForRepository)
            {
                if (!_assigmentService.DeleteAssignment(assigment))
                    return false;
            }

            if (!authorizationResult.Succeeded && userRole.RoleID != 1)
            {
                return false;
            }

            var userRepositoryToDelate = _context.UsersRepository.Where(u => u.RepositoryID == repository.RepositoryID).ToList();

            foreach( var user in userRepositoryToDelate)
            {
                _context.Remove(user);
            }

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

  

        public ICollection<Repository> GetRepositoryForUser(int userId)
        {
           
           
            ICollection<UserRepository> userRepositories = _context.UsersRepository.Where(x => x.UserID == userId).ToList();
            ICollection<Repository> userRepositoriesList = new List<Repository>();


            foreach (UserRepository userRepository in userRepositories)
            {
              Repository repository =  _context.Repository.Where(r => r.RepositoryID == userRepository.RepositoryID).FirstOrDefault();
                if (repository != null)
                {
                    userRepositoriesList.Add(repository);
                }
            }

            return userRepositoriesList;
        }




        public UserRepository UserConfirmAndExist(int userID, int repositoryID)
        {
            var userRepository = _context.UsersRepository.Where(ur => ur.UserID == userID && ur.RepositoryID == repositoryID).FirstOrDefault();

            if (userRepository != null)
            {
                return userRepository;

            }

            UserRepository userRepo = new UserRepository()
            {
                IsMember = false,
                UserID = 0,
                RepositoryID = 0,
                Privilage = 0,
                EnterDate = DateTime.Now
            };
            return userRepo;

        }

        public ICollection<UserRepository> GetStudentsToConfirm(int repositoryID)
        {
            return _context.UsersRepository.Where(ur => ur.IsMember==false && ur.RepositoryID == repositoryID).ToList();
        }

        public ICollection<UserRepository> GetAcceptedStudents(int repositoryID)
        {
            return _context.UsersRepository.Where(ur => ur.IsMember == true && ur.RepositoryID == repositoryID).ToList();
        }


        public bool ConfirmUser(int userID, int repositoryID)
        {
            UserRepository user = _context.UsersRepository.Where(ur => ur.UserID == userID && ur.RepositoryID == repositoryID).FirstOrDefault();

            if(user != null)
            {
                user.IsMember = true;
                Save();
                return true;
            }
            else { return false; }


        }

        public UserRepository GetUserRepository(int repositoryID, int userID) {
            return _context.UsersRepository.Where(ur => ur.UserID == userID && ur.RepositoryID == repositoryID).FirstOrDefault();

        }

    }
}
