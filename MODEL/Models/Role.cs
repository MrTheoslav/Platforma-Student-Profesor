
using System.ComponentModel.DataAnnotations;

namespace MODEL.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
