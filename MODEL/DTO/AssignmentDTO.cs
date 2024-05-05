using System.ComponentModel.DataAnnotations;

namespace MODEL.DTO
{
    public class AssignmentDTO
    {
        public int AssignmentID { get; set; }

        [Required(ErrorMessage = "Podaj nazwę zadania.")]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        public int FileSize { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RepositoryID { get; set; }
    }
}
