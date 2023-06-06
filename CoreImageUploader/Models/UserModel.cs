using System.ComponentModel.DataAnnotations;

namespace CoreImageUploader.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Image is required")]
        public IFormFile? UserImage { get; set; }

    }
}
