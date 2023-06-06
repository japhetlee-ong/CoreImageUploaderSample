using CoreImageUploader.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CoreImageUploader.Controllers
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly Regex sWhitespace = new Regex(@"\s+");

        public UploadController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel _user)
        {
            if (ModelState.IsValid)
            {
                var imageDirectory = Path.Combine(webHostEnvironment.WebRootPath, "images");
                var name = ReplaceWhitespace(_user.Name,"");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var fileName = Guid.NewGuid().ToString() + "_" + name + "_" + _user.UserImage?.FileName;
                var filePath = Path.Combine(imageDirectory, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                _user.UserImage?.CopyTo(stream);
                Debug.WriteLine(imageDirectory);
                Debug.WriteLine(filePath);

            }

            return View();
        }

        [NonAction]
        private string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}
