using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.Data;
using WAD.Models;

namespace WAD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ImagesController : Controller
    {
        public readonly DataContext _dbContext;
        private readonly IWebHostEnvironment _environment;


        public ImagesController(DataContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _environment = env;
        }

        

        [HttpPost]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            bool Result = false;
            try
            {
                var _uploadedFiles = Request.Form.Files;
                foreach (IFormFile source in _uploadedFiles)
                {
                    string FileName = source.FileName;
                    string FilePath = GetFilePath(FileName);
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    string imagePath = FilePath + "\\image.png";
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagePath))
                    {
                        await source.CopyToAsync(stream);
                        Result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
              return Ok(Result);
            
        }
        [HttpGet]
        public async Task<ActionResult> GetImages(string directoryPath)
        {
            try
            {
                string imagePath = directoryPath + ".png";
                var files = GetFilePath(imagePath);
                return Ok(files);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [NonAction]
        private string GetFilePath(string imageCode)
        {
            return this._environment.WebRootPath + imageCode;
        }
    }
}
