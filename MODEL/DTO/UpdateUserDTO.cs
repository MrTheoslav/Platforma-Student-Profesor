using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public  class UpdateUserDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CurrentPassword { get; set; }
        public int RoleId { get; set; } = 3;

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
