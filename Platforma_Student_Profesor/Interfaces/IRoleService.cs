using MODEL.Models;

namespace API.Interfaces
{
    public interface IRoleService
    {
        bool CreateRole(Role role);
        bool DeleteRole(Role role);
        ICollection<Role> GetAllRoles();
        Role GetRoleByID(int id);
        bool Save();
        bool UpdateRole(Role role);
        bool RoleExist(int id);
    }
}