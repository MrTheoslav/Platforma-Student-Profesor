using API.Interfaces;
using API.Settings;
using DAL;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using MODEL.Models;
using System.ComponentModel;

namespace API.Services
{
    public class FileService : IFileService
    {
        private readonly DataContext _context;
        private static AppSettings _appSettings;
        private static string filePath => _appSettings.FileDirectory;

        public FileService(DataContext context, IOptionsMonitor<AppSettings> appSettings)
        {
            _appSettings = appSettings.CurrentValue;
            _context = context;
        }
        public async Task<bool> WriteFile(UserAssigmnent userAssigmnent, IFormFile file)
        {
            try
            {
                var fullFilePath = GetFilePath(userAssigmnent);

                if (!Directory.Exists(fullFilePath))
                {
                    Directory.CreateDirectory(fullFilePath);
                }

                var exactPath = Path.Combine(fullFilePath, file.FileName);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fullFilePath = Path.Combine(fullFilePath, file.FileName);

                userAssigmnent.Files = fullFilePath;

                _context.Update(userAssigmnent);

                return _context.SaveChanges() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetFilePath(UserAssigmnent userAssigmnent)
        {
            var repID = GetRepository(userAssigmnent.AssigmnentID).RepositoryID;
            var assID = userAssigmnent.AssigmnentID;
            var usID = userAssigmnent.UserID;
            return Path.Combine(filePath, repID.ToString(), assID.ToString(), usID.ToString());
        }

        public async Task<(FileStream, string, string)> DownloadFile(UserAssigmnent userAssigmnent)
        {

            var fullFilePath = GetFilePath(userAssigmnent);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fullFilePath, out var contentType))
                contentType = "application/octet-stream";

            var file = File.OpenRead(fullFilePath);

            return (file, contentType, Path.GetFileName(filePath));
        }

        public Repository GetRepository(int assignmentID)
        {
            var repID = _context.Assignments.Where(a => a.AssignmentID == assignmentID).FirstOrDefault().RepositoryID;
            return _context.Repository.Where(r => r.RepositoryID == repID).FirstOrDefault();
        }

        public ICollection<UserAssigmnent> GetUserAssigmnents(int assignmentID)
        {
            return _context.UserAssigmnents.Where(ua => ua.AssigmnentID == assignmentID).ToList();
        }

        public ICollection<UserAssigmnent> GetUserAssigmnents(int assignmentID, int userId)
        {
            return _context.UserAssigmnents.Where(ua => ua.AssigmnentID == assignmentID && ua.UserID == userId).ToList();
        }

        public UserAssigmnent GetUserAssigmnent(int assignmentID, int userId, string file)
        {
            return _context.UserAssigmnents.Where(ua => ua.AssigmnentID == assignmentID && ua.UserID == userId && ua.Files == file).FirstOrDefault();
        }
    }
}
