using MODEL.Models;

namespace API.Interfaces
{
    public interface IFileService
    {
        Task<(FileStream, string, string)> DownloadFile(UserAssigmnent userAssigmnent);
        string GetFilePath(UserAssigmnent userAssigmnent);
        Repository GetRepository(int assignmentID);
        ICollection<UserAssigmnent> GetUserAssigmnents(int assignmentID);
        ICollection<UserAssigmnent> GetUserAssigmnents(int assignmentID, int userId);
        UserAssigmnent GetUserAssigmnent(int assignmentID, int userId, string file);
        Task<bool> WriteFile(UserAssigmnent userAssigmnent, IFormFile file);
    }
}
