using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MimeTypes;
using smart_tour_api.Models;
using smart_tour_api.Servicies;

namespace smart_tour_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;
        public FilesController(IWebHostEnvironment env, IUserService userService)
        {
            _env = env;
            _userService = userService;
        }
        //da fare
        [HttpGet("resource")]
        [AllowAnonymous]
        public async Task<ActionResult<List<InternalMediaModel>>> getFile(int idAgency, string filename)
        {
            if (filename == null)
            {
                return BadRequest(new { message = "File name is null" });
            }
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "resourceFile", idAgency.ToString(), filename);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, MimeTypeMap.GetMimeType(Path.GetExtension(path)), Path.GetFileName(path));
            }
            catch (IOException)
            {
                return null;
            }
        }
        [HttpGet("filenames")]
        public async Task<ActionResult<List<InternalMediaModel>>> GetFilenames()
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "resourceFile", idAgency.ToString());
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                List<InternalMediaModel> mediaInfo=new List<InternalMediaModel>();
                foreach(FileInfo file in directoryInfo.EnumerateFiles())
                {
                    InternalMediaModel item = new InternalMediaModel
                    {
                        Name = file.Name,
                        Type = MimeTypeMap.GetMimeType(file.Extension)
                    };
                    mediaInfo.Add(item);
                }

                return mediaInfo;
            }
            catch (IOException)
            {
                return null;
            }
        }
        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            if (filename == null)
            {
                return BadRequest(new { message = "File name is null" });
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "resourceFile", idAgency.ToString(), filename);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

            return File(memory, MimeTypeMap.GetMimeType(Path.GetExtension(path)), Path.GetFileName(path));
            }
            catch (IOException)
            {
                return null;
            }
            
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                return BadRequest(new { message = "file not loaded" });
            }

            try
            {
                Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, "resourceFile",idAgency.ToString()));
            }
            catch (IOException)
            {
                return null;
            }
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "resourceFile", idAgency.ToString(), uploadedFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(stream);
                }

            return Ok();
            }
            catch (IOException)
            {
                return null;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(string filename)
        {
            if (filename == null)
            {
                return BadRequest(new { message = "File name is null" });
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            
            try
            {
                String path = Path.Combine(_env.ContentRootPath, "resourceFile", idAgency.ToString(), filename);

                if (!System.IO.File.Exists(path))
                {
                    return BadRequest(new { message = "File name not exists" });
                }
                System.IO.File.Delete(path);

                return Ok();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}