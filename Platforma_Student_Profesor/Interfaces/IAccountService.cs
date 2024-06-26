﻿using MODEL.DTO;
using MODEL.Models;

namespace API.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        bool Save();

        string GenerateJwt(LoginDto dto);
        User GetUser();
        User GetUserById(int id);

        ICollection<User> GetUsersToConfirm();

        bool ConfirmUser( int userID);

        void UpdateUser(UpdateUserDTO dto);
        ICollection<User> GetConfirmedUser();
        bool IsApproved(int userID);
        bool DeleteUser(UserDTO dto);
    }
}