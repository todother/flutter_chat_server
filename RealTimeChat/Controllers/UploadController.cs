using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> uploadFiles([FromForm(Name ="file")] IFormFile file,string name,string sender)
        {
            var uploaded = file;
            string path = "./wwwroot/voiceTemp";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string savePath = path + "/" + name ;
            using(var stream = System.IO.File.Create(savePath))
            {
                await uploaded.CopyToAsync(stream);
            }

            Process.Start("bash");
            string fileType = name.Split(".").ToList().Last();
            if (fileType == "m4a")
            {
                Process.Start("ffmpeg", " -i " + savePath + "  -acodec libmp3lame -aq 2 " + savePath.Replace(".m4a", ".mp3")).WaitForExit();
            }
            else
            {
                Process.Start("ffmpeg", " -i " + savePath + "  " + savePath.Replace(".mp4", ".mp3")).WaitForExit();
            }
            System.IO.File.Delete(savePath);


            return Ok(new { filePath = savePath });
        }
    }
}
