using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AbsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/hinhanh")]
    public class HinhAnhController : Controller
    {
        private readonly IWebHostEnvironment environment;
        public HinhAnhController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost()]
        public async Task<string> UploadImage(IFormFile hinhAnh)
        {
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string[] fileName = hinhAnh.FileName.Split(".");
                string nameImage = fileName[0] + ".jpg";
                string Filepath = GetFilepath();
                if (!Directory.Exists(Filepath))
                {
                    Directory.CreateDirectory(Filepath);
                }

                string imagepath = Filepath + nameImage;
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await hinhAnh.CopyToAsync(stream);
                }
                return nameImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("multip")]
        public async Task<List<string>> MultiUploadImage([FromForm]IFormFileCollection hinhAnhs)
        {
            try
            {
                var results = new List<string>();
                string Filepath = GetFilepath();
                if (!Directory.Exists(Filepath))
                {
                    Directory.CreateDirectory(Filepath);
                }
                foreach (var file in hinhAnhs)
                {
                    string[] fileName = file.FileName.Split(".");
                    string nameImage = fileName[0] + ".jpg";
                    string imagepath = Filepath + nameImage;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        results.Add(nameImage);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private string GetFilepath()
        {
            return this.environment.WebRootPath + "\\Upload\\image\\";
        }
    }
}
