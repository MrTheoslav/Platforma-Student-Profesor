namespace MODEL.DTO
{
    public class UserRepositoryDTO
    {
        public DateTime EnterDate { get; set; }
        public bool HasPrivilage { get; set; }
        public bool IsMember { get; set; }
        public int UserID { get; set; }
        public int RepositoryID { get; set; }
    }
}
