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

        public AccountService(DataContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IRoleService roleService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _roleService = roleService;
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
                UserLastName = dto.UserLastName

            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.Password = hashedPassword;
            _context.Add(newUser);
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
                new Claim(ClaimTypes.NameIdentifier, user.RoleID.ToString()),
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
    }
}
