using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.Data;
using WAD.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase 
    {
        public class FileUpload
        {
            public IFormFile files { get; set; } 
        }

        public static IWebHostEnvironment _environment;
        private readonly DataContext _dbConnection;


        public ImageController (IWebHostEnvironment environment, DataContext dbConnection)
        {
            _environment = environment;
            _dbConnection = dbConnection;
        }



        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var imageData = ms.ToArray();
                var image = new ImageModel { Data = imageData };
                _dbConnection.Images.Add(image);
                _dbConnection.SaveChanges();
            }
            return Ok("saved");
        }

        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            var image = _dbConnection.Images.FirstOrDefault(i => i.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            return File(image.Data, "image/jpeg"); // replace "image/jpeg" with the appropriate MIME type for your image format
        }


        /*  

        [HttpPost]
        public async Task<string> PostFile(FileUpload objFile) 
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\Upload\\" + objFile.files.FileName;
                    }
                }
                else
                {
                    return "Bd type of files";
                }
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();   
            }
           
        }
        */
    }
}
