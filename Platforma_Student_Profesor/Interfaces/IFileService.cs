using MODEL.Models;

namespace API.Interfaces
{
    public interface IFileService
    {
        string GetRepositoryName(int assignmentID);
        Task<bool> WriteFile(string filePath, UserAssigmnent userAssigmnent, IFormFile file);
    }
}
