using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.Models
{
    public class UserAssigmnent
    {
        public int AssigmnentID { get; set; }
        public int UserID { get; set; }
        public bool IsCreated { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime SendDate { get; set; }
        public User User { get; set; }
        public Assignment Assignment { get; set; }
    }
}
