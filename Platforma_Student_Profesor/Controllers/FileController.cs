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
    //[Authorize]
    [ApiController]
    [Route("API/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _webHostEnvironment;
        private static AppSettings _appSettings;
        private static string filePath => _appSettings.FileDirectory;

        public FileController(IOptionsMonitor<AppSettings> appSettings ,IFileService fileService, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.CurrentValue;
            _mapper = mapper;
        }
        //[Authorize(Roles = "admin,teacher,student")]
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(TransferFile transferFile)
        {
            var userAssigmnent = transferFile.UserAssigmnentDTO;
            var file = transferFile.file;

            if (userAssigmnent == null)
                return BadRequest("Brak podanych informacji na temat zadania/ucznia przesyłającego zadanie");

            if (file == null)
                return BadRequest("Brak wysłanego pliku");

            if(!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak podczas przesyłania pliku.");

            //Check if student sent assigmnent

            var userAssigmnentMap = _mapper.Map<UserAssigmnent>(userAssigmnent);

            if (!await _fileService.WriteFile(filePath, userAssigmnentMap, file))
                return BadRequest("Plik nie został poprawnie przesłany");

            return Ok($"Przesłano plik {file.FileName}.");
        }
    }
}
