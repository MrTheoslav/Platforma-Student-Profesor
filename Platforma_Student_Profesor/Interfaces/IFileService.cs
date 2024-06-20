using MODEL.Models;

namespace API.Interfaces
{
    public interface IFileService
    {
        Task<(FileStream, string)> DownloadFile(MODEL.Models.File infoAboutSender);
        string GetFilePath(MODEL.Models.File infoAboutSender);
        Repository GetRepository(int assignmentID);
        ICollection<MODEL.Models.File> GetInfoAboutSenders(int assignmentID);
        ICollection<MODEL.Models.File> GetInfoAboutSenders(int assignmentID, int userId);
        MODEL.Models.File GetInfoAboutSender(int assignmentID, int userId, string file);
        Task<bool> WriteFile(MODEL.Models.File infoAboutSender, IFormFile file);
        bool FileExists(MODEL.Models.File file);
        bool RemoveUserFile(MODEL.Models.File file);
    }
}
