using API.Interfaces;
using DAL;
using MODEL.Models;

namespace API.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateRole(Role role)
        {
            var saved = _context.Add(role);
            return Save();
        }

        public bool UpdateRole(Role role)
        {
            var saved = _context.Update(role);
            return Save();
        }

        public bool DeleteRole(Role role)
        {
            _context.Remove(role);
            return Save();
        }

        public ICollection<Role> GetAllRoles()
        {
            return _context.Roles.OrderBy(r => r.Name).ToList();
        }

        public Role GetRoleByID(int id)
        {
            return _context.Roles.Where(r => r.RoleID == id).FirstOrDefault();
        }

        public bool RoleExist(int id)
        {
            return _context.Roles.Any(r => r.RoleID == id);
        }


    }
}
