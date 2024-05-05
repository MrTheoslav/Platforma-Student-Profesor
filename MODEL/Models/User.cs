using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName {  get; set; }
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Password { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime EnterDate { get; set; }
        public bool IsApproved { get; set; }
        public Role Role { get; set; }
        public int RoleID { get; set; }
        public ICollection<UserAssigmnent> UserAssigmnents { get; set; }
        public ICollection<UserRepository> UserRepositories { get; set; }
    }
}
