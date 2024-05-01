using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.Models
{
    public class UserRepository
    {
        [Column(TypeName = ("DATETIME"))]
        public DateTime EnterDate { get; set; }
        public string Privilage { get; set; }
        public bool IsMember { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public Repository Repository { get; set; }
        public int RepositoryID {  get; set; }
    }
}
