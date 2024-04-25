using System.ComponentModel.DataAnnotations;

namespace MODEL.DTO
{
    public class RepositoryDTO
    {
        public int RepositoryID { get; set; }

        [Required(ErrorMessage = "Podaj nazwę repozytorium.")]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        public string Topic { get; set; }
    }
}
