using System.ComponentModel.DataAnnotations;

namespace MODEL.DTO
{
    public class UserAssigmnentDTO
    {
        public int UserAssigmnentID { get; set; }
        public int AssigmnentID { get; set; }
        public int UserID { get; set; }
        public double Mark { get; set; }
        public string? Comment { get; set; }
        public DateTime SendDate { get; set; }
    }
}
