using API.Interfaces;
using DAL;
using MODEL.Models;
using System.ComponentModel;

namespace API.Services
{
    public class FileService : IFileService
    {
        private readonly DataContext _context;
        public FileService(DataContext context) 
        {
            _context = context;
        }
        public async Task<bool> WriteFile(string filePath, UserAssigmnent userAssigmnent, IFormFile file)
        {
            try
            {
                //var fullFilePath = Path.Combine(filePath, $"{GetRepositoryName(userAssigmnent.AssigmnentID)}", $"{userAssigmnent.Assignment.Name}", $"{userAssigmnent.User.UserID}");

                var fullFilePath = filePath;

                if (!Directory.Exists(fullFilePath))
                {
                    Directory.CreateDirectory(fullFilePath);
                }

                var exactPath = Path.Combine(fullFilePath, file.FileName);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string GetRepositoryName(int assignmentID)
        {
            return _context.Repository.Where(r => r.RepositoryID == _context.Assignments.Where(a => a.AssignmentID == assignmentID).FirstOrDefault().RepositoryID).FirstOrDefault().Name;
        }
    }
}
