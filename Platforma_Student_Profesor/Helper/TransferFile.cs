using MODEL.Models;

namespace API.Helper
{
    public class TransferFile
    {
        public FileDTO file { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
