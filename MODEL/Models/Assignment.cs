﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentID { get; set; }
        [Required]
        public string Name { get; set; }
        public int FileSize { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime EndDate { get; set; }
     
        public Repository Repository { get; set; }
        public ICollection<UserAssigmnent> UserAssigmnents { get; set; }
        public int RepositoryID { get; set; }

        public ICollection<File> Files { get; set; }
    }
}
