using System.ComponentModel.DataAnnotations;

namespace MODEL.Models
{
    public class Repository
    {
        [Key]
        public int RepositoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Topic { get; set; }
        public int? CreatedById { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<UserRepository> UserRepositories { get; set; }
    }
}
