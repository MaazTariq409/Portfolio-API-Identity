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
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                string wwwrootpath = _webHost.WebRootPath;
                string filename = Guid.NewGuid().ToString();
                var Uploadpath = Path.Combine(wwwrootpath, @"Images\");
                var extension = Path.GetExtension(file.FileName);

                if (file != null)
                {

                    var fileStream = new FileStream(Path.Combine(Uploadpath, filename + extension), FileMode.Create);

                    file.CopyTo(fileStream);

                    var dbPath = Path.Combine(@"Images\" + filename + extension);

                    return Ok(new { imageDbPath = dbPath});
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
