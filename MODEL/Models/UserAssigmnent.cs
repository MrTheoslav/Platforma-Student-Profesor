using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.Models
{
    public class UserAssigmnent
    {
        public int AssigmnentID { get; set; }
        public int UserID { get; set; }
        public double Mark { get; set; }
        
        public string? Comment { get; set; }
        public string? Files { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime SendDate { get; set; }
        public User User { get; set; }
        public Assignment Assignment { get; set; }
    }
}
