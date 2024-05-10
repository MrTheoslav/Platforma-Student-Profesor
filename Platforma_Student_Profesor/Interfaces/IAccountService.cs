using MODEL.DTO;
using MODEL.Models;

namespace API.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        bool Save();

        string GenerateJwt(LoginDto dto);
        User GetUser();
    }
}