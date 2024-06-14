using MODEL.Models;

namespace API.Interfaces
{
    public interface IFileService
    {
        Repository GetRepository(int assignmentID);
        Task<bool> WriteFile(string filePath, UserAssigmnent userAssigmnent, IFormFile file);
    }
}
