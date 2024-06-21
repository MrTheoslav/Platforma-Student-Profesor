using API.Helper;
using API.Interfaces;
using API.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MODEL.DTO;
using MODEL.Models;

namespace API.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [ApiController]
    [Route("API/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _webHostEnvironment;

        public FileController(IFileService fileService, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        [Authorize(Roles = "admin,teacher,student")]
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(TransferFile transferFile)
        {
            FileDTO infoAboutSender = new FileDTO
            {
                AssigmentID = transferFile.AssigmentID,
                UserID = transferFile.UserID,
            };
            var files = transferFile.files;
            int count = 0;
            foreach (var file in files)
            {

                if (infoAboutSender == null)
                    return BadRequest("Brak podanych informacji na temat zadania/ucznia przesyłającego zadanie");

                if (file == null)
                    return BadRequest("Brak wysłanego pliku");

                if (!ModelState.IsValid)
                    return BadRequest("Coś poszło nie tak podczas przesyłania pliku.");

                var fileMap = _mapper.Map<MODEL.Models.File>(infoAboutSender);

                if (_fileService.FileExists(fileMap))
                    return BadRequest("Podany plik istnieje. Spróbuj zmienić nazwę pliku");

                if (await _fileService.WriteFile(fileMap, file))
                    count++;

            }
            if (count == 0)
                return BadRequest("Żaden plik nie został przesłany poprawnie");
            return Ok($"Liczba przesłanych plików: {count}");
        }

        [HttpGet]
        [Route("FilesNames/{assigmnentID}")]
        public IActionResult GetAllFilesNamesForAssigmnent(int assigmnentID)
        {
            var fileDTOs = _mapper.Map<List<FileDTO>>(_fileService.GetInfoAboutSenders(assigmnentID));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            return Ok(fileDTOs);
        }

        [HttpGet]
        [Route("FilesNames/{assigmnentID}/{userID}")]
        public IActionResult GetAllFilesNamesForAssigmnentForUser(int assigmnentID, int userID)
        {
            var fileDTOs = _mapper.Map<List<FileDTO>>(_fileService.GetInfoAboutSenders(assigmnentID, userID));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            return Ok(fileDTOs);
        }

        [HttpGet]
        [Route("DownloadFile/{assigmnentID}/{userID}/{fileName}")]
        public async Task<IActionResult> DownloadFile(int assigmnentID, int userID, string fileName)
        {
            var infoAboutSender = _fileService.GetInfoAboutSender(assigmnentID, userID, fileName);

            (FileStream fileStream, string contentType) = await _fileService.DownloadFile(infoAboutSender);

            return File(fileStream, contentType, fileName);
        }

        [HttpDelete]
        [Route("RemoveFile")]
        public IActionResult RemoveFile(FileDTO fileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            var file = _fileService.GetFile(fileDTO.UserID, fileDTO.AssigmentID,fileDTO.FileName);

            if (file == null)
                return NotFound("Nie znaleziono pliku do usunięcia");

            if (!_fileService.RemoveUserFile(file))
                return BadRequest("Coś poszło nie tak podczas usuwania pliku");

            return Ok("Plik został poprawnie usunięty");
        }

    }
}
