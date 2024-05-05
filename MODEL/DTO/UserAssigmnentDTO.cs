namespace MODEL.DTO
{
    public class UserAssigmnentDTO
    {
        public int AssigmnentID { get; set; }
        public int UserID { get; set; }
        public double Mark { get; set; }
        public string Comment { get; set; }
        public string Files { get; set; }
        public DateTime SendDate { get; set; }
    }
}
