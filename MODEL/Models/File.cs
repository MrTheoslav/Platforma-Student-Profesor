using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class File
    {
        [Key]
        public int FileID{ get; set; }
        public int UserID {  get; set; }
        public int AssigmentID {  get; set; }
        public string? FileName { get; set; }
        public User User { get; set; }
        public Assignment Assignment { get; set; }
    }
}
