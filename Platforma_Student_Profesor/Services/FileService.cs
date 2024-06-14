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
                var repID = GetRepository(userAssigmnent.AssigmnentID).RepositoryID;
                var assID = userAssigmnent.AssigmnentID;
                var usID = userAssigmnent.UserID;
                var fullFilePath = Path.Combine(filePath, repID.ToString(), assID.ToString(), usID.ToString());

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
            catch//(Exception)
            {
                return false;
            }
        }
        public Repository GetRepository(int assignmentID)
        {
            var repID = _context.Assignments.Where(a=>a.AssignmentID == assignmentID).FirstOrDefault().RepositoryID;
            return _context.Repository.Where(r => r.RepositoryID == repID).FirstOrDefault();
        }
    }
}
