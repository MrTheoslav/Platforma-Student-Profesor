namespace API.Helper
{
    public class UserAndAssignment
    {
        public int AssigmnentID { get; set; }
        public int UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public double Mark { get; set; }
        public string? Comment { get; set; }
        public DateTime SendDate { get; set; }
    }
}
