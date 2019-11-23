using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Hubs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealTimeChat.Controllers
{
    public class UploadController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> uploadFiles([FromForm(Name ="file")] IFormFile file,string name)
        {
            var uploaded = file;
            string path = "../voiceTemp";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string savePath = path + "/" + name + ".mp3";
            using(var stream = System.IO.File.Create(savePath))
            {
                await uploaded.CopyToAsync(stream);
            }
            return Ok(new { filePath = savePath });
        }
    }
}
