using API.Interfaces;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MODEL.DTO;
using MODEL.Models;
using System.Data;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;

        public IPasswordHasher<User> _passwordHasher;

        public AuthenticationSettings _authenticationSettings;
        private readonly IRoleService _roleService;
        private readonly IUserContextService _userContextService;

        public AccountService(DataContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IRoleService roleService, IUserContextService userContextService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _roleService = roleService;
            _userContextService = userContextService;
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public void RegisterUser(RegisterUserDto dto)

        {
            var newUser = new User()
            {
                Email = dto.Email,
                RoleID = dto.RoleId,
                UserFirstName = dto.UserFirstName,
                UserLastName = dto.UserLastName,
                EnterDate = DateTime.Now,
                

            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.Password = hashedPassword;
            _context.Add(newUser);
            Save();

        }

        public void UpdateUser(UpdateUserDTO dto)
        {
            
            var user = _context.Users.SingleOrDefault(u => u.UserID == dto.UserID);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

         
            user.Email = dto.Email;
            user.UserFirstName = dto.UserFirstName;
            user.UserLastName = dto.UserLastName;

          
            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.Password = hashedPassword;

         
            _context.Update(user);

           
            Save();
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u=>u.Role)
                .FirstOrDefault(u=>u.Email ==dto.Email );

            if(user is  null)
            {
                return "userIsNull";
            }

            var result = _passwordHasher.VerifyHashedPassword(user,user.Password,dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                return "PasswordNotCorrect";
            }

            var userRole = _roleService.GetRoleByID(user.RoleID);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, $"{user.UserFirstName},{user.UserLastName}"),
                new Claim(ClaimTypes.Role, $"{userRole.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);


            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);



            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }


        public User GetUser()
        {
            return _context.Users.Where(u => u.UserID == _userContextService.GetUserId).FirstOrDefault();
            
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(u => u.UserID == id).FirstOrDefault();

        }

        public ICollection<User> GetUsersToConfirm()
        {
            return _context.Users.Where(u => u.IsApproved == false).ToList();
        }

        public ICollection<User> GetConfirmedUser()
        {
            return _context.Users.Where(u => u.IsApproved == true).ToList();
        }

        public bool ConfirmUser(int userID)
        {
            User user = GetUserById(userID);

                user.IsApproved = true;
                Save();
                return true;
            
           

        }

        public bool IsApproved(int userID)
        {
            User user = GetUserById(userID);
            if(user != null) { return user.IsApproved; }

            else { return false; }
            
           
        }

    }
}
