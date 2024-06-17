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
        public async Task<bool> WriteFile(MODEL.Models.File infoAboutSender, IFormFile file)
        {
            try
            {
                var fullFilePath = Path.Combine(filePath ,GetFilePath(infoAboutSender));

                if (!Directory.Exists(fullFilePath))
                {
                    Directory.CreateDirectory(fullFilePath);
                }

                var exactPath = Path.Combine(fullFilePath, file.FileName);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                infoAboutSender.FileName= file.FileName;

                _context.Add(infoAboutSender);

                return _context.SaveChanges() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetFilePath(MODEL.Models.File infoAboutSender)
        {
            var repID = GetRepository(infoAboutSender.AssigmentID).RepositoryID;
            var assID = infoAboutSender.AssigmentID;
            var usID = infoAboutSender.UserID;
            return Path.Combine(repID.ToString(), assID.ToString(), usID.ToString());
        }

        public async Task<(FileStream, string)> DownloadFile(MODEL.Models.File infoAboutSender)
        {

            var fullFilePath = Path.Combine(filePath, GetFilePath(infoAboutSender) ,infoAboutSender.FileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fullFilePath, out var contentType))
                contentType = "application/octet-stream";

            var file = System.IO.File.OpenRead(fullFilePath);

            return (file, contentType);
        }

        public Repository GetRepository(int assignmentID)
        {
            var repID = _context.Assignments.Where(a => a.AssignmentID == assignmentID).FirstOrDefault().RepositoryID;
            return _context.Repository.Where(r => r.RepositoryID == repID).FirstOrDefault();
        }

        public ICollection<MODEL.Models.File> GetInfoAboutSenders(int assignmentID)
        {
            return _context.Files.Where(f => f.AssigmentID == assignmentID).ToList();
        }

        public ICollection<MODEL.Models.File> GetInfoAboutSenders(int assignmentID, int userId)
        {
            return _context.Files.Where(f => f.AssigmentID == assignmentID && f.UserID == userId).ToList();
        }

        public MODEL.Models.File GetInfoAboutSender(int assignmentID, int userId, string fileName)
        {
            return _context.Files.Where(f => f.AssigmentID == assignmentID && f.UserID == userId && f.FileName == fileName).FirstOrDefault();
        }

    }
}
