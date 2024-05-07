using MODEL.DTO;

namespace API.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        bool Save();

        string GenerateJwt(LoginDto dto);
    }
}