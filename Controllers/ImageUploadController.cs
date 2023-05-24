using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Portfolio_API.Controllers
{
    [Route("api/uploadimage")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHost;

        public ImageUploadController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            string wwwrootpath = _webHost.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var Uploadpath = Path.Combine(wwwrootpath, @"images\");
                var extension = Path.GetExtension(file.FileName);

                //if (obj.Product.ImageUrl != null)
                //{
                //    var oldImagePath = Path.Combine(wwwrootpath, obj.Product.ImageUrl.TrimStart('\\'));
                //    if (System.IO.File.Exists(oldImagePath))
                //    {
                //        System.IO.File.Delete(oldImagePath);
                //    }
                //}

                var fileStream = new FileStream(Path.Combine(Uploadpath, filename + extension), FileMode.Create);

                file.CopyTo(fileStream);

                return Ok(new { imageUrl = @"images\" + filename + extension });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
