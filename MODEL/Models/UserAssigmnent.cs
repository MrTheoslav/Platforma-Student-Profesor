using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.Models
{
    public class UserAssigmnent
    {
        [Key]
        public int AssigmnentID { get; set; }
        [Key]
        public int UserID { get; set; }
        public double Mark { get; set; }
        public string? Comment { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime SendDate { get; set; }
        public User User { get; set; }
        public Assignment Assignment { get; set; }
    }
}
